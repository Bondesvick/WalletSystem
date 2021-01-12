using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Dtos.Transaction
{
    public class GetTransactionDto
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }

        public int WalletId { get; set; }

        public string CurrencyCode { get; set; }

        public int CurrencyId { get; set; }
    }
}