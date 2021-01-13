using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.Wallet
{
    /// <summary>
    ///
    /// </summary>
    public class CreateWalletDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "Wallet currency id is required")]
        public int CurrencyId { get; set; }
    }
}