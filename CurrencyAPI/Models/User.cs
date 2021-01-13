using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WalletSystemAPI.Models
{
    /// <summary>
    ///
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        ///
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<Wallet> Wallets { get; set; }
    }
}