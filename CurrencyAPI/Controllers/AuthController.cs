using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(RegisterUserDto userToRegisterUserDto)
        {
            return Ok();
        }

        [HttpPost("LogIn")]
        public IActionResult Login(RegisterUserDto userToRegisterUserDto)
        {
            return Ok();
        }
    }
}