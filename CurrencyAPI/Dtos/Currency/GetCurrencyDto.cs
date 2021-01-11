using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Currency
{
    public class GetCurrencyDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
    }
}