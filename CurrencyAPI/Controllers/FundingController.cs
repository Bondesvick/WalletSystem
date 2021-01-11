using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Funding;
using WalletSystemAPI.Helpers;

namespace WalletSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundingController : ControllerBase
    {
        public FundingController()
        {
        }

        [HttpPost("ApproveFunding")]
        public IActionResult ApproveFunding(ApproveFundingDto approveFundingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            return Ok();
        }
    }
}