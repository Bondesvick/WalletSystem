using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<UserRepository> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _config = configuration;
            _logger = logger;
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _signManager = serviceProvider.GetRequiredService<SignInManager<User>>();
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public GetUserDto MapUser(string id)
        {
            var user = GetUserById(id);
            var userToReturn = _mapper.Map<GetUserDto>(user);

            return userToReturn;
        }

        public GetUserDto GetMyDetails()
        {
            return MapUser(GetUserId());
        }

        public User GetUserById(string id)
        {
            return _userManager.Users.FirstOrDefault(user => user.Id == id);
        }

        public Task<User> GetUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public void AddUserToRole(User user, string role)
        {
            _userManager.AddToRoleAsync(user, role);
        }

        public List<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<string> LoginUser(UserToLoginDto userToLoginDto)
        {
            var user = await GetUserByEmail(userToLoginDto.Email);
            var roles = await GetUserRoles(user);

            await _signManager.SignInAsync(user, false);

            return JwtTokenConfig.GetToken(user, _config, roles);
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = GetUserById(id);
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public Task<IList<string>> GetUserRoles(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public async Task<bool> ChangeUserRole(User user, string oldRole, string newRole)
        {
            var removed = await _userManager.RemoveFromRoleAsync(user, oldRole);
            var added = await _userManager.AddToRoleAsync(user, newRole);

            return removed.Succeeded && added.Succeeded;
        }
    }
}