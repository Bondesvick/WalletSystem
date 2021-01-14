using System;
using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.Wallet
{
    /// <summary>
    ///
    /// </summary>
    public class WithdrawalDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public int WalletId { get; set; }

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