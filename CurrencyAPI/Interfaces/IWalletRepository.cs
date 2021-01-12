using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    public interface IWalletRepository
    {
        bool AddWallet(Wallet wallet);

        Task<bool> DeleteWallet(int id);

        Task<bool> UpdateWallet(Wallet wallet);

        bool CheckWallet(int walletId);

        List<Wallet> GetAllMyWallets();

        Wallet GetWalletById(int id);

        List<Wallet> GetWalletsById(int id);

        List<Wallet> GetWalletsByUserId(string ownerId);

        List<Wallet> GetAllWallets();

        Task<bool> FundWallet(FundingDto fundingDto);

        Task<bool> FundNoobWallet(FundingDto fundingDto);

        Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto);

        Task<bool> ChangeMainCurrency(Wallet oldWallet, Wallet newWallet);
    }
}