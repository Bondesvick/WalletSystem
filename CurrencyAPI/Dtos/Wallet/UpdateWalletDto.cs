using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Wallet
{
    /// <summary>
    ///
    /// </summary>
    public class UpdateWalletDto : CreateWalletDto
    {
        /// <summary>
        ///
        /// </summary>
        public int WalletId { get; set; }
    }
}