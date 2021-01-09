﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        public UserRepository(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<UserRepository> logger, IMapper mapper)
        {
            _config = configuration;
            _logger = logger;
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _signManager = serviceProvider.GetRequiredService<SignInManager<User>>();
            _mapper = mapper;
        }

        public User GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}