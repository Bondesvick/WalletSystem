using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WalletSystemAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("MainCurrencyId")]
        public Currency MainCurrency { get; set; }

        public int MainCurrencyId { get; set; }

        public ICollection<Wallet> Wallets { get; set; }
    }
}