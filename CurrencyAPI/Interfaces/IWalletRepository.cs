using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    public interface IWalletRepository
    {
        bool AddWallet(Wallet wallet);

        bool DeleteWallet(int id);

        Wallet GetWalletById(int id);

        List<Wallet> GetAllWallets();

        bool FundWallet(FundingDto fundingDto);

        bool WithdrawFromWallet(WithdrawalDto withdrawalDto);
    }
}