using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        ///
        /// </summary>
        /// <param name="currencyRepository"></param>
        /// <param name="mapper"></param>
        public CurrencyController(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all country codes
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCurrencies")]
        public IActionResult GetAllCurrencies()
        {
            var currencies = _currencyRepository.GetAllCurrencies();
            var data = currencies.Select(c => _mapper.Map<GetCurrencyDto>(c)).ToList();

            return Ok(ResponseMessage.Message("List of all Currencies and their slug code", null, data));
        }
    }
}