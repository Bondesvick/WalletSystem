﻿namespace WalletSystemAPI.Dtos.Funding
{
    /// <summary>
    ///
    /// </summary>
    public class GetFundingDto
    {
        /// <summary>
        ///
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int FundingId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int WalletId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}