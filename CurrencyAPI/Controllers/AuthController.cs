using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Dtos.User;
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
        private readonly IWalletRepository _walletRepository;

        public AuthController(IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(RegisterUserDto userToRegisterUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Make sure the required fields are filled properly", ModelState));

            if (userToRegisterUserDto.Role == "Admin" || userToRegisterUserDto.Role != "Elite" || userToRegisterUserDto.Role != "Nood")
                return BadRequest(ResponseMessage.Message("Invalid User Role", "User role can only be Noob or Elite", userToRegisterUserDto));

            var checkUser = await _userRepository.GetUserByEmail(userToRegisterUserDto.Email);
            if (checkUser != null)
                return BadRequest(ResponseMessage.Message("User with the email already exist"));

            User user = new User()
            {
                FirstName = userToRegisterUserDto.FirstName,
                LastName = userToRegisterUserDto.LastName,
                UserName = userToRegisterUserDto.Email,
                Email = userToRegisterUserDto.Email,
                PhoneNumber = userToRegisterUserDto.PhoneNumber,
                Address = userToRegisterUserDto.Address
            };

            var succeeded = await _userRepository.RegisterUser(user, userToRegisterUserDto.Password);
            if (!succeeded)
                return BadRequest(ResponseMessage.Message("Unable register User"));

            _userRepository.AddUserToRole(user, userToRegisterUserDto.Role);

            Wallet wallet = new Wallet()
            {
                Balance = 0,
                CurrencyId = userToRegisterUserDto.MainCurrencyId,
                IsMain = true,
                OwnerId = user.Id
            };

            var created = _walletRepository.AddWallet(wallet);

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

        [HttpGet("GetUserDetail/{id}")]
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