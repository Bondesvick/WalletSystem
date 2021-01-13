namespace WalletSystemAPI.Dtos.Wallet
{
    /// <summary>
    ///
    /// </summary>
    public class GetWalletDto
    {
        /// <summary>
        ///
        /// </summary>
        public int WalletId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsMain { get; set; }
    }
}