using System.Collections.Generic;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Transaction> GetMyTransactions();

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="walletId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        bool CreateTransaction(TransactionType type, decimal amount, int walletId, int currencyId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteTransaction(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        bool CheckTransaction(int transactionId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Transaction GetTransactionById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        List<Transaction> GetWalletTransactions(int walletId);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Transaction> GetAllTransactions();
    }
}