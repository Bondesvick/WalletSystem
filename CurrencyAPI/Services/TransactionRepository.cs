using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletSystemAPI.Data;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public List<Transaction> GetMyTransactions()
        {
            return _context.Transactions.Include(t => t.Wallet).Where(t => t.Wallet.OwnerId == GetUserId()).ToList();
        }

        public bool CreateTransaction(TransactionType type, decimal amount, int walletId, int currencyId)
        {
            Transaction transaction = new Transaction
            {
                Type = type,
                Amount = amount,
                Date = DateTime.Now,
                WalletId = walletId,
                CurrencyId = currencyId
            };

            try
            {
                _context.Transactions.AddAsync(transaction);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTransaction(int id)
        {
            try
            {
                var transaction = GetTransactionById(id);
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckTransaction(int transactionId)
        {
            return _context.Transactions.Any(t => t.Id == transactionId);
        }

        public Transaction GetTransactionById(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public List<Transaction> GetWalletTransactions(int walletId)
        {
            return _context.Transactions.Where(t => t.WalletId == walletId).ToList();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }
    }
}