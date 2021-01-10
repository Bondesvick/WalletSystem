using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

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
        public IActionResult Login(UserToLoginDto userToLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userRepository.LoginUser(userToLoginDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();

            GetUserDto userToReturn = _userRepository.MapUser(id);
            response.Data = userToReturn;
            response.Message = "Success";
            response.Success = true;

            return Ok(response);
        }
    }
}