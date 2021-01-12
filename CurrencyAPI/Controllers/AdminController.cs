using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Dtos.Funding;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFundRepository _fundRepository;
        private readonly IWalletRepository _walletRepository;

        public AdminController(IFundRepository fundRepository, IWalletRepository walletRepository)
        {
            _fundRepository = fundRepository;
            _walletRepository = walletRepository;
        }

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

        [HttpPost("ChangeUserAccountType")]
        public IActionResult ChangeUserAccountType(ChangeUserAccountTypeDto changeUserAccountTypeDto)
        {
            return Ok();
        }

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