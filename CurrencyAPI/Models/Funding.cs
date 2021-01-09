﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Models
{
    public class Funding
    {
        public int Id { get; set; }

        public Decimal Amount { get; set; }

        public string CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [ForeignKey("DestinationId")]
        public Wallet Destination { get; set; }

        public string DestinationId { get; set; }

        public bool IsApproved { get; set; }
    }
}