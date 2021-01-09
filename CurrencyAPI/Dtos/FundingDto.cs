﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos
{
    public class FundingDto
    {
        public string UserId { get; set; }
        public int WalletId { get; set; }
        public string CurrencyId { get; set; }
        public Decimal Amount { get; set; }
    }
}