using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Data;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;

        public WalletRepository(DataContext context)
        {
            _context = context;
        }

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
            var wallet = await GetWalletById(id);
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

        public bool UpdateWallet(Wallet wallet)
        {
            try
            {
                _context.Wallets.Update(wallet);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ValueTask<Wallet> GetWalletById(int id)
        {
            return _context.Wallets.FindAsync(id);
        }

        public List<Wallet> GetAllWallets()
        {
            return _context.Wallets.ToList();
        }

        public async Task<bool> FundWallet(FundingDto fundingDto)
        {
            var wallet = await GetWalletById(fundingDto.WalletId);
            wallet.Balance += fundingDto.Amount;

            return UpdateWallet(wallet);
        }

        public async Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto)
        {
            var wallet = await GetWalletById(withdrawalDto.WalletId);
            wallet.Balance -= withdrawalDto.Amount;

            return UpdateWallet(wallet);
        }
    }
}