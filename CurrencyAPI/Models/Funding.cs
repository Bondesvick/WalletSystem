using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Funding
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int CurrencyId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ForeignKey("DestinationId")]
        public Wallet Destination { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int DestinationId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsApproved { get; set; } = false;
    }
}