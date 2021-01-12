using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;

        public WalletController(IWalletRepository walletRepository, ICurrencyRepository currencyRepository, IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
        }

        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletDto walletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, walletDto));

            var userWallets = _walletRepository.GetWalletsByUserId(walletDto.OwnerId);

            var user = _userRepository.GetUserById(walletDto.OwnerId);
            var userRoles = await _userRepository.GetUserRoles(user);

            if (userRoles.Contains("Noob") && userWallets.Count > 0)
                return BadRequest(ResponseMessage.Message("Already has a wallet",
                    "your account type is only allowed to have o wallet", walletDto));

            var wallet = new Wallet()
            {
                CurrencyId = walletDto.CurrencyId,
                OwnerId = walletDto.OwnerId
            };

            var created = _walletRepository.AddWallet(wallet);

            if (!created)
                return BadRequest(ResponseMessage.Message("Unable to create wallet",
                    "error encountered while creating wallet", walletDto));

            return Ok(ResponseMessage.Message("Wallet successfully created", null, walletDto));
        }

        [HttpDelete("DeleteWallet/{id}")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var deleted = await _walletRepository.DeleteWallet(id);

            if (!deleted)
                return BadRequest(ResponseMessage.Message("Unable to delete wallet", "error encountered while deleting the wallet", id));

            return Ok(ResponseMessage.Message("Wallet successfully deleted", null, id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateWallet(UpdateWalletDto walletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, walletDto));

            var wallet = _walletRepository.GetWalletById(walletDto.WalletId);

            if (wallet == null)
                return BadRequest(ResponseMessage.Message("Unable to update wallet", "invalid wallet id", walletDto));

            wallet.CurrencyId = walletDto.CurrencyId;

            var updated = await _walletRepository.UpdateWallet(wallet);

            if (!updated)
                return BadRequest(ResponseMessage.Message("Unable to update wallet", "error encountered while updating the wallet", walletDto));

            return Ok(ResponseMessage.Message("Wallet successfully updated", null, walletDto));
        }

        [HttpGet("GetWalletDetail/{id}")]
        public IActionResult GetWallet(int id)
        {
            var wallet = _walletRepository.GetWalletById(id);

            if (wallet == null)
                return BadRequest(ResponseMessage.Message("Wallet not found", "invalid wallet id", id));

            var theWallet = new GetWalletDto()
            {
                WalletId = wallet.Id,
                CurrencyCode = wallet.Currency.Code,
                Balance = wallet.Balance,
                OwnerId = wallet.OwnerId
            };

            return Ok(ResponseMessage.Message("Successful!", null, theWallet));
        }

        [HttpPost("FundWallet")]
        public async Task<IActionResult> FundWallet(FundingDto fundingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, fundingDto));

            var currencyExist = _currencyRepository.CurrencyExist(fundingDto.CurrencyId);

            if (!currencyExist)
                return NotFound(ResponseMessage.Message("Currency Not found", "currency id provided is invalid", fundingDto));

            var user = _userRepository.GetUserById(fundingDto.UserId);
            var userRoles = await _userRepository.GetUserRoles(user);

            if (userRoles.Contains("Noob"))
            {
                var noobWalletFunded = await _walletRepository.FundNoobWallet(fundingDto);

                if (!noobWalletFunded)
                    return BadRequest(ResponseMessage.Message("Unable to fund wallet", "An error was encountered while trying to fund the wallet", fundingDto));
            }

            var walletFunded = await _walletRepository.FundWallet(fundingDto);

            if (!walletFunded)
                return BadRequest(ResponseMessage.Message("Unable to fund wallet", "An error was encountered while trying to fund the wallet", fundingDto));

            return Ok(ResponseMessage.Message("Wallet successfully funded", null, fundingDto));
        }

        [HttpPost("WithdrawFromWallet")]
        public async Task<IActionResult> WithdrawFromWallet(WithdrawalDto withdrawalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, withdrawalDto));

            var currencyExist = _currencyRepository.CurrencyExist(withdrawalDto.CurrencyId);

            if (!currencyExist)
                return NotFound(ResponseMessage.Message("Currency Not found", "currency id provided is invalid", withdrawalDto));

            var walletFunded = await _walletRepository.WithdrawFromWallet(withdrawalDto);

            if (!walletFunded)
                return BadRequest(ResponseMessage.Message("Unable to withdraw from wallet", "An error was encountered while trying to withdraw from the wallet", withdrawalDto));

            return Ok(ResponseMessage.Message("You have successfully debited the walled", null, withdrawalDto));
        }

        [HttpGet("GettAllMyWallets")]
        public IActionResult GetAllMyWallets()
        {
            var myWallets = _walletRepository.GetAllMyWallets();

            var wallets = myWallets.Select(w => new GetWalletDto()
            {
                WalletId = w.Id,
                CurrencyCode = w.Currency.Code,
                Balance = w.Balance,
                OwnerId = w.OwnerId
            }).ToList();

            return Ok(ResponseMessage.Message("List of all wallets you own", null, wallets));
        }
    }
}