using System;
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

        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [ForeignKey("DestinationId")]
        public Wallet Destination { get; set; }

        public int DestinationId { get; set; }

        public bool IsApproved { get; set; }
    }
}