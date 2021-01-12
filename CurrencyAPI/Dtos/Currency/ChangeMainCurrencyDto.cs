using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Currency
{
    public class ChangeMainCurrencyDto
    {
        public int OldMainCurrencyId { get; set; }
        public int NewMainCurrencyId { get; set; }
    }
}