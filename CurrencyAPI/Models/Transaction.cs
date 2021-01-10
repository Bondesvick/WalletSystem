using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WalletSystemAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        [Required]
        public int WalletId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [Required]
        public int CurrencyId { get; set; }
    }

    public enum TransactionType
    {
        Credit,
        Debit
    }
}