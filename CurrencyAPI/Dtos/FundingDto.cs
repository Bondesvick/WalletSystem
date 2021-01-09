using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos
{
    public class FundingDto
    {
        public string CurrencyId { get; set; }
        public Decimal Amount { get; set; }
        public string DestinationWalletId { get; set; }
    }
}