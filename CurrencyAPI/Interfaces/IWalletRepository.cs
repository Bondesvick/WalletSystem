﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IWalletRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> MergeAllWalletsToMain(User user);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetUserId();

        /// <summary>
        ///
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        bool AddWallet(Wallet wallet);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteWallet(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        Task<bool> UpdateWallet(Wallet wallet);

        /// <summary>
        ///
        /// </summary>
        /// <param name="updateWalletDto"></param>
        /// <returns></returns>
        Task<bool> UpdateWallet(UpdateWalletDto updateWalletDto);

        /// <summary>
        ///
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        bool CheckWallet(int walletId);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Wallet> GetAllMyWallets();

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Wallet GetWalletById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Wallet> GetWalletsById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        List<Wallet> GetWalletsByUserId(string ownerId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Wallet GetUserMainCurrencyWallet(string userId);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Wallet> GetAllWallets();

        /// <summary>
        ///
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="amount"></param>
        Task<bool> FundWallet(Wallet wallet, decimal amount);

        /// <summary>
        ///
        /// </summary>
        /// <param name="main"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<bool> FundWallet(Wallet main, Wallet source);

        /// <summary>
        ///
        /// </summary>
        /// <param name="funding"></param>
        /// <returns></returns>
        Task<bool> FundNoobWallet(Funding funding);

        /// <summary>
        ///
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool CanWithdrawFromWallet(decimal balance, decimal? amount);

        /// <summary>
        ///
        /// </summary>
        /// <param name="withdrawalDto"></param>
        /// <returns></returns>
        Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto);

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldWallet"></param>
        /// <param name="newWallet"></param>
        /// <returns></returns>
        Task<bool> ChangeMainCurrency(Wallet oldWallet, Wallet newWallet);

        /// <summary>
        ///
        /// </summary>
        /// <param name="fundingDto"></param>
        /// <returns></returns>
        bool UserHasWalletWithCurrency(FundingDto fundingDto);

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Wallet GetUserWalletByCurrencyId(string userId, int currencyId);
    }
}