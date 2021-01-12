using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public int CurrencyId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public string OwnerId { get; set; }

        public bool IsMain { get; set; }

        public IList<Transaction> Transactions { get; set; }
    }
}