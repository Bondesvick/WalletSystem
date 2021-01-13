using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Funding
{
    /// <summary>
    ///
    /// </summary>
    public class ApproveFundingDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public int FundingId { get; set; }
    }
}