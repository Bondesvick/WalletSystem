using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.Wallet
{
    public class UpdateWalletDto : CreateWalletDto
    {
        public int WalletId { get; set; }
    }
}