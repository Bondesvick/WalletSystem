using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.Wallet
{
    public class CreateWalletDto
    {
        [Required(ErrorMessage = "Wallet currency id is required")]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "Wallet owner Id is required")]
        public string OwnerId { get; set; }
    }
}