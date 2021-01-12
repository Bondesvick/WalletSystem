using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WalletSystemAPI.Data;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IFundRepository _fundRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WalletRepository(DataContext context, ICurrencyRepository currencyRepository, IFundRepository fundRepository, ITransactionRepository transactionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _currencyRepository = currencyRepository;
            _fundRepository = fundRepository;
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

        public List<Wallet> GetAllMyWallets()
        {
            return _context.Wallets.Include(w => w.Currency).Where(w => w.OwnerId == GetUserId()).ToList();
        }

        public Wallet GetWalletById(int id)
        {
            return _context.Wallets.Include(w => w.Currency).FirstOrDefault(w => w.Id == id);
        }

        public List<Wallet> GetWalletsById(int id)
        {
            return _context.Wallets.Include(w => w.Currency).Where(w => w.Id == id).ToList();
        }

        public List<Wallet> GetWalletsByUserId(string ownerId)
        {
            return _context.Wallets.Where(w => w.OwnerId == ownerId).ToList();
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

                _transactionRepository.CreateTransaction(TransactionType.Credit, fundingDto.Amount, fundingDto.WalletId,
                    fundingDto.CurrencyId);
            }
            else
            {
                var targetCode = _currencyRepository.GetCurrencyCode(wallet.CurrencyId);
                var sourceCode = _currencyRepository.GetCurrencyCode(fundingDto.CurrencyId);

                var newAmount = await CurrencyRate.ConvertCurrency(sourceCode, targetCode, fundingDto.Amount);

                wallet.Balance += newAmount ?? 0;

                _transactionRepository.CreateTransaction(TransactionType.Credit, newAmount ?? 0, fundingDto.WalletId, fundingDto.CurrencyId);
            }

            return await UpdateWallet(wallet);
        }

        public async Task<bool> FundNoobWallet(FundingDto fundingDto)
        {
            return await _fundRepository.CreateFunding(fundingDto);
        }

        public async Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto)
        {
            var wallet = GetWalletById(withdrawalDto.WalletId);

            if (wallet.CurrencyId == withdrawalDto.CurrencyId)
            {
                wallet.Balance -= withdrawalDto.Amount;

                _transactionRepository.CreateTransaction(TransactionType.Debit, withdrawalDto.Amount, withdrawalDto.WalletId,
                    withdrawalDto.CurrencyId);
            }
            else
            {
                var targetCode = _currencyRepository.GetCurrencyCode(wallet.CurrencyId);
                var sourceCode = _currencyRepository.GetCurrencyCode(withdrawalDto.CurrencyId);

                var newAmount = await CurrencyRate.ConvertCurrency(sourceCode, targetCode, withdrawalDto.Amount);

                wallet.Balance -= newAmount ?? 0;

                _transactionRepository.CreateTransaction(TransactionType.Debit, newAmount ?? 0, withdrawalDto.WalletId, withdrawalDto.CurrencyId);
            }

            return await UpdateWallet(wallet);
        }

        public async Task<bool> ChangeMainCurrency(Wallet oldWallet, Wallet newWallet)
        {
            oldWallet.IsMain = false;
            newWallet.IsMain = true;

            return await UpdateWallet(oldWallet) && await UpdateWallet(newWallet);
        }
    }
}