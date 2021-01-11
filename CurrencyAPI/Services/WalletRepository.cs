using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WalletSystemAPI.Data;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WalletRepository(DataContext context, ITransactionRepository transactionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _transactionRepository = transactionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public bool AddWallet(Wallet wallet)
        {
            try
            {
                _context.Wallets.AddAsync(wallet);
                _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteWallet(int id)
        {
            var wallet = GetWalletById(id);
            try
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateWallet(Wallet wallet)
        {
            try
            {
                _context.Wallets.Update(wallet);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckWallet(int walletId)
        {
            return _context.Wallets.Any(w => w.Id == walletId);
        }

        public Wallet GetWalletById(int id)
        {
            return _context.Wallets.FirstOrDefault(w => w.Id == id);
        }

        public List<Wallet> GwWalletsById(int id)
        {
            return _context.Wallets.Where(w => w.Id == id).ToList();
        }

        public List<Wallet> GetAllWallets()
        {
            return _context.Wallets.ToList();
        }

        public async Task<bool> FundWallet(FundingDto fundingDto)
        {
            var wallet = GetWalletById(fundingDto.WalletId);

            if (fundingDto.CurrencyId == wallet.CurrencyId)
            {
                wallet.Balance += fundingDto.Amount;
            }
            else
            {
            }

            return await UpdateWallet(wallet);
        }

        public Task<bool> FundNoobWallet(Funding funding)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto)
        {
            var wallet = GetWalletById(withdrawalDto.WalletId);
            wallet.Balance -= withdrawalDto.Amount;

            return await UpdateWallet(wallet);
        }
    }
}