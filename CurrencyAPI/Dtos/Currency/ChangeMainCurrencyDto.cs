using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.Currency
{
    /// <summary>
    ///
    /// </summary>
    public class ChangeMainCurrencyDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public int OldMainCurrencyWalletId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int NewMainCurrencyWalletId { get; set; }
    }
}