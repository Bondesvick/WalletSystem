namespace WalletSystemAPI.Dtos.Wallet
{
    public class GetWalletDto
    {
        public int WalletId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
        public string OwnerId { get; set; }
        public bool IsMain { get; set; }
    }
}