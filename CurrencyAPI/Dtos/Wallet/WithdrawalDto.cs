using System;

namespace WalletSystemAPI.Dtos.Wallet
{
    public class WithdrawalDto
    {
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public Decimal Amount { get; set; }
        public int WalletId { get; set; }
    }
}