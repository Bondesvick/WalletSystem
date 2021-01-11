using System;

namespace WalletSystemAPI.Dtos.Wallet
{
    public class FundingDto
    {
        public string UserId { get; set; }
        public int WalletId { get; set; }
        public int CurrencyId { get; set; }
        public Decimal Amount { get; set; }
    }
}