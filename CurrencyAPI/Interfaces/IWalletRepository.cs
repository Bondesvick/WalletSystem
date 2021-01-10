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

        Task<bool> UpdateWallet(Wallet wallet);

        bool CheckWallet(int walletId);

        Wallet GetWalletById(int id);

        List<Wallet> GwWalletsById(int id);

        List<Wallet> GetAllWallets();

        Task<bool> FundWallet(FundingDto fundingDto);

        Task<bool> FundNoobWallet(Funding funding);

        Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto);
    }
}