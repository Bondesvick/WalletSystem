using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    public interface ITransactionRepository
    {
        bool CreateTransaction(TransactionType type, decimal amount, int walletId, int currencyId);

        bool DeleteTransaction(int id);

        bool CheckTransaction(int transactionId);

        Transaction GetTransactionById(int id);

        List<Transaction> GetWalletTransactions(int walletId);

        List<Transaction> GetAllTransactions();
    }
}