using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WalletSystemAPI.Dtos.Transaction;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Allows only logged-in Elite and Noob account holders to get all transactions made on every of their wallet
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Elite, Noob")]
        [HttpGet]
        public IActionResult GetMyTransaction()
        {
            var transactions = _transactionRepository.GetMyTransactions();
            var myTransactions = transactions.Select(t => new GetTransactionDto()
            {
                Amount = t.Amount,
                CurrencyCode = t.Currency.Code,
                CurrencyId = t.CurrencyId,
                Date = t.Date,
                Type = t.Type
            });

            return Ok(ResponseMessage.Message("List of all my transaction", null, myTransactions));
        }
    }
}