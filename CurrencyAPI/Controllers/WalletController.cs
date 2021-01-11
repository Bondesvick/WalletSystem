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

            return Ok();
        }

        [HttpDelete("DeleteWallet/{id}")]
        public IActionResult DeleteWallet(int id)
        {
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult UpdateWallet(CreateWalletDto walletDto)
        {
            return Ok();
        }

        [HttpGet("GetWalletDetail/{id}")]
        public IActionResult GetWallet(int id)
        {
            return Ok();
        }

        [HttpPost("FundWallet")]
        public IActionResult FundWallet(FundingDto fundingDto)
        {
            return Ok();
        }
    }
}