using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletSystemAPI.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Transaction
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public TransactionType Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int WalletId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int CurrencyId { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        ///
        /// </summary>
        Credit,

        /// <summary>
        ///
        /// </summary>
        Debit
    }
}