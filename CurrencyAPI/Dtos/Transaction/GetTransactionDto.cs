﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Dtos.Transaction
{
    /// <summary>
    ///
    /// </summary>
    public class GetTransactionDto
    {
        /// <summary>
        ///
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public TransactionType Type { get; set; }

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
        public int CurrencyId { get; set; }
    }
}