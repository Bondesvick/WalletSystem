using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Wallet
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IList<Transaction> Transactions { get; set; }
    }
}