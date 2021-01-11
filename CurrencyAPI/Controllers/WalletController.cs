using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpPost("CreateWallet")]
        public IActionResult CreateWallet(CreateWalletDto walletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

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

            return Ok(ResponseMessage.Message("Successful!", null, wallet));
        }

        [HttpPost("FundWallet")]
        public IActionResult FundWallet(FundingDto fundingDto)
        {
            _walletRepository.FundWallet(fundingDto);
            return Ok();
        }
    }
}