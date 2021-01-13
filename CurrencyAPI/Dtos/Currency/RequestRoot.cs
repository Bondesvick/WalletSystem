namespace WalletSystemAPI.Dtos.Currency
{
    /// <summary>
    ///
    /// </summary>
    public class RequestRoot
    {
        /// <summary>
        ///
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Timestamp { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Rates Rates { get; set; }
    }
}