using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Currency
{
    public class ChangeMainCurrencyDto
    {
        [Required]
        public int OldMainCurrencyWalletId { get; set; }

        [Required]
        public int NewMainCurrencyWalletId { get; set; }
    }
}