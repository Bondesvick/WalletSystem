using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos
{
    public class GetWalletDto
    {
        public int WalletId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
        public string OwnerId { get; set; }
    }
}