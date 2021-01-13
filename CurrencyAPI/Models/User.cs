using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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