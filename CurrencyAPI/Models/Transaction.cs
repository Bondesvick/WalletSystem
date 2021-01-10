using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        public int WalletId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public string OwnerId { get; set; }
    }

    public enum TransactionType
    {
        Credit,
        Debit
    }
}