using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Currency
{
    /// <summary>
    ///
    /// </summary>
    public class ChangeMainCurrencyDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public int OldMainCurrencyWalletId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int NewMainCurrencyWalletId { get; set; }
    }
}