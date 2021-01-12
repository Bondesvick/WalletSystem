using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public CurrencyController(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAllCurrencies")]
        public IActionResult GetAllCurrencies()
        {
            var currencies = _currencyRepository.GetAllCurrencies();
            var data = currencies.Select(c => _mapper.Map<GetCurrencyDto>(c)).ToList();

            return Ok(ResponseMessage.Message("List of all Currencies and their slug code", null, data));
        }
    }
}