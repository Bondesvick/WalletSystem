using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;

namespace WalletSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        public WalletController()
        {
        }

        [HttpPost]
        public IActionResult CreateWallet(CreateWalletDto walletDto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWallet(int id)
        {
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult UpdateWallet(CreateWalletDto walletDto)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetWallet(int id)
        {
            return Ok();
        }
    }
}