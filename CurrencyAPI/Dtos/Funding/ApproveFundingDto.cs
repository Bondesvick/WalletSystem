using System.ComponentModel.DataAnnotations;

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