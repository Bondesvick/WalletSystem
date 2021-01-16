using System;
using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.Wallet
{
    /// <summary>
    ///
    /// </summary>
    public class FundNoobDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public string WalletOwnerId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int CurrencyId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public Decimal Amount { get; set; }
    }
}