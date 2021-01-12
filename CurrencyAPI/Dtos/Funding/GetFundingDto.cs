using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Funding
{
    public class GetFundingDto
    {
        public decimal Amount { get; set; }
        public int FundingId { get; set; }
        public int WalletId { get; set; }
        public string CurrencyCode { get; set; }
    }
}