using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Funding
{
    public class ApproveFundingDto
    {
        [Required]
        public int FundingId { get; set; }
    }
}