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

        Task<bool> DeleteWallet(int id);

        bool UpdateWallet(Wallet wallet);

        bool CheckWallet(int walletId);

        ValueTask<Wallet> GetWalletById(int id);

        List<Wallet> GwWalletsById(int id);

        List<Wallet> GetAllWallets();

        Task<bool> FundWallet(FundingDto fundingDto);

        Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto);
    }
}