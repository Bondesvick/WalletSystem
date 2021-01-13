using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Dtos.Funding;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFundRepository _fundRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public AdminController(IFundRepository fundRepository, IWalletRepository walletRepository, IUserRepository userRepository)
        {
            _fundRepository = fundRepository;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUnApprovedFundings")]
        public IActionResult GetUnApprovedFundings()
        {
            var fundings = _fundRepository.GetUnApprovedFundings();

            var toFund = fundings.Select(f => new GetFundingDto()
            {
                Amount = f.Amount,
                CurrencyCode = f.Currency.Code,
                FundingId = f.Id,
                WalletId = f.DestinationId,
            }).ToList();

            return Ok(ResponseMessage.Message("List of all Nood fundings yet to be approved", null, toFund));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ChangeUserMainCurrency")]
        public async Task<IActionResult> ChangeUserMainCurrency(ChangeMainCurrencyDto changeMainCurrencyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            var old = _walletRepository.GetWalletById(changeMainCurrencyDto.OldMainCurrencyWalletId);
            var @new = _walletRepository.GetWalletById(changeMainCurrencyDto.NewMainCurrencyWalletId);

            if (old == null || @new == null)
                return BadRequest(ResponseMessage.Message("one of the ids entered is incorrect", "wallet to found", changeMainCurrencyDto));

            var changed = await _walletRepository.ChangeMainCurrency(old, @new);
            if (!changed)
                return BadRequest(ResponseMessage.Message("Unable to change main currency", "error encountered while trying to save main currency", changeMainCurrencyDto));

            return Ok(ResponseMessage.Message("Main currency changed successfully", null, changeMainCurrencyDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ChangeUserAccountType")]
        public async Task<IActionResult> ChangeUserAccountType(ChangeUserAccountTypeDto changeUserAccountTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            var user = _userRepository.GetUserById(changeUserAccountTypeDto.UserId);
            if (user == null)
                return BadRequest(ResponseMessage.Message("Invalid user Id", "user with the id was not found", changeUserAccountTypeDto));

            if (changeUserAccountTypeDto.NewType != "Noob" && changeUserAccountTypeDto.NewType != "Elite")
                return BadRequest(ResponseMessage.Message("Invalid User Role", "User role can only be Noob or Elite", changeUserAccountTypeDto));

            var roles = await _userRepository.GetUserRoles(user);
            var oldRole = roles.FirstOrDefault();

            if (oldRole == null)
            {
                _userRepository.AddUserToRole(user, changeUserAccountTypeDto.NewType);
            }
            else
            {
                await _userRepository.ChangeUserRole(user, oldRole, changeUserAccountTypeDto.NewType);
            }

            return Ok(ResponseMessage.Message("Account type changed successfully", null, changeUserAccountTypeDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ApproveFunding")]
        public async Task<IActionResult> ApproveFunding(ApproveFundingDto approveFundingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            var funding = _fundRepository.GetFundingById(approveFundingDto.FundingId);
            if (funding == null)
                return BadRequest(ResponseMessage.Message("Invalid funding Id", "funding with the id was not found", approveFundingDto));

            FundingDto fundingDto = new FundingDto()
            {
                Amount = funding.Amount,
                CurrencyId = funding.CurrencyId,
                UserId = funding.Destination.OwnerId,
                WalletId = funding.DestinationId
            };

            var funded = await _walletRepository.FundWallet(fundingDto);

            if (!funded)
                return BadRequest(ResponseMessage.Message("Unable to fund account", "and error was encountered while trying to fund this account", approveFundingDto));

            await _fundRepository.DeleteFunding(approveFundingDto.FundingId);

            return Ok(ResponseMessage.Message("Noob account funded", null, approveFundingDto));
        }
    }
}