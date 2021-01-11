using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Helpers;
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
        public async Task<IActionResult> SignUp(RegisterUserDto userToRegisterUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Make sure the required fields are filled properly", ModelState));

            var checkUser = await _userRepository.GetUserByEmail(userToRegisterUserDto.Email);
            if (checkUser != null)
                return BadRequest(ResponseMessage.Message("User with the email already exist"));

            User user = new User()
            {
                FirstName = userToRegisterUserDto.FirstName,
                LastName = userToRegisterUserDto.LastName,
                UserName = userToRegisterUserDto.Email,
                Email = userToRegisterUserDto.Email,
                MainCurrencyId = userToRegisterUserDto.MainCurrencyId,
                PhoneNumber = userToRegisterUserDto.PhoneNumber,
                Address = userToRegisterUserDto.Address
            };

            var succeeded = await _userRepository.RegisterUser(user, userToRegisterUserDto.Password);
            if (!succeeded)
                return BadRequest(ResponseMessage.Message("Unable register User"));

            _userRepository.AddUserToRole(user, userToRegisterUserDto.Role);
            return Ok(ResponseMessage.Message("Account Created", null, userToRegisterUserDto));
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> Login(UserToLoginDto userToLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Fill out all the files", ModelState));

            var checkUser = await _userRepository.GetUserByEmail(userToLoginDto.Email);
            if (checkUser == null)
                return BadRequest(ResponseMessage.Message("User does not exist"));

            var token = await _userRepository.LoginUser(userToLoginDto);

            return Ok(ResponseMessage.Message("You account has been logged-in", null, token));
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