using System;
using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.Wallet
{
    public class WithdrawalDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int WalletId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public Decimal Amount { get; set; }
    }
}