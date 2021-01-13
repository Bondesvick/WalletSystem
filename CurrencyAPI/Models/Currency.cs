using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Currency
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}