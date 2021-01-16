using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    /// <summary>
    ///
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetUserDto MapUser(string id)
        {
            var user = GetUserById(id);
            var userToReturn = _mapper.Map<GetUserDto>(user);

            return userToReturn;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public GetUserDto GetMyDetails() => MapUser(GetUserId());

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(string id) => _userManager.Users.FirstOrDefault(user => user.Id == id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<User> GetUserByEmail(string email) => _userManager.FindByEmailAsync(email);

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void AddUserToRole(User user, string role) => _userManager.AddToRoleAsync(user, role);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers() => _userManager.Users.ToList();

        /// <summary>
        ///
        /// </summary>
        /// <param name="userToLoginDto"></param>
        /// <returns></returns>
        public async Task<string> LoginUser(UserToLoginDto userToLoginDto)
        {
            var user = await GetUserByEmail(userToLoginDto.Email);
            var roles = await GetUserRoles(user);

            await _signManager.SignInAsync(user, false);

            return JwtTokenConfig.GetToken(user, _config, roles);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string id)
        {
            var user = GetUserById(id);
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<string>> GetUserRoles(User user) => _userManager.GetRolesAsync(user);

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldRole"></param>
        /// <param name="newRole"></param>
        /// <returns></returns>
        public async Task<bool> ChangeUserRole(User user, string oldRole, string newRole)
        {
            var removed = await _userManager.RemoveFromRoleAsync(user, oldRole);
            var added = await _userManager.AddToRoleAsync(user, newRole);

            return removed.Succeeded && added.Succeeded;
        }
    }
}