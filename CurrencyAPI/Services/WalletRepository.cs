using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WalletSystemAPI.Data;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    /// <summary>
    ///
    /// </summary>
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IFundRepository _fundRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="currencyRepository"></param>
        /// <param name="fundRepository"></param>
        /// <param name="transactionRepository"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        public WalletRepository(DataContext context, ICurrencyRepository currencyRepository, IFundRepository fundRepository, ITransactionRepository transactionRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _context = context;
            _currencyRepository = currencyRepository;
            _fundRepository = fundRepository;
            _transactionRepository = transactionRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        ///
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="updateWalletDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWallet(UpdateWalletDto updateWalletDto)
        {
            var walletToUpdate = GetWalletById(updateWalletDto.WalletId);

            if (walletToUpdate.CurrencyId != updateWalletDto.CurrencyId)
            {
                var targetCode = _currencyRepository.GetCurrencyCode(updateWalletDto.CurrencyId);
                var sourceCode = _currencyRepository.GetCurrencyCode(walletToUpdate.CurrencyId);

                var newAmount = await CurrencyRate.ConvertCurrency(sourceCode, targetCode, walletToUpdate.Balance);

                walletToUpdate.CurrencyId = updateWalletDto.CurrencyId;
                walletToUpdate.Balance = newAmount ?? 0;
            }

            return await UpdateWallet(walletToUpdate);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        public bool CheckWallet(int walletId)
        {
            return _context.Wallets.Any(w => w.Id == walletId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Wallet> GetAllMyWallets()
        {
            return _context.Wallets.Include(w => w.Currency).Where(w => w.OwnerId == GetUserId()).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Wallet GetWalletById(int id)
        {
            return _context.Wallets.Include(w => w.Currency).FirstOrDefault(w => w.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Wallet> GetWalletsById(int id)
        {
            return _context.Wallets.Include(w => w.Currency).Where(w => w.Id == id).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public List<Wallet> GetWalletsByUserId(string ownerId)
        {
            return _context.Wallets.Where(w => w.OwnerId == ownerId).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Wallet GetUserMainCurrencyWallet(string userId)
        {
            return _context.Wallets.FirstOrDefault(w => w.OwnerId == userId && w.IsMain);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Wallet> GetAllWallets()
        {
            return _context.Wallets.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fundingDto"></param>
        /// <returns></returns>
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
                var user = _userRepository.GetUserById(fundingDto.UserId);
                var roles = await _userRepository.GetUserRoles(user);

                var targetCode = _currencyRepository.GetCurrencyCode(wallet.CurrencyId);
                var sourceCode = _currencyRepository.GetCurrencyCode(fundingDto.CurrencyId);

                var newAmount = await CurrencyRate.ConvertCurrency(sourceCode, targetCode, fundingDto.Amount);

                wallet.Balance += newAmount ?? 0;

                _transactionRepository.CreateTransaction(TransactionType.Credit, newAmount ?? 0, fundingDto.WalletId, fundingDto.CurrencyId);
            }

            return await UpdateWallet(wallet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fundingDto"></param>
        /// <returns></returns>
        public async Task<bool> FundNoobWallet(FundingDto fundingDto)
        {
            return await _fundRepository.CreateFunding(fundingDto);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CanWithdrawFromWallet(decimal balance, decimal? amount)
        {
            return (balance - amount) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<bool> WithdrawFromMain(string userId, decimal amount)
        {
            var mainWallet = GetUserMainCurrencyWallet(userId);

            if (!CanWithdrawFromWallet(mainWallet.Balance, amount))
            {
                return false;
            }

            mainWallet.Balance -= amount;

            return await UpdateWallet(mainWallet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="withdrawalDto"></param>
        /// <returns></returns>
        public async Task<bool> WithdrawFromWallet(WithdrawalDto withdrawalDto)
        {
            var wallet = GetWalletById(withdrawalDto.WalletId);

            var user = _userRepository.GetUserById(withdrawalDto.UserId);
            var roles = await _userRepository.GetUserRoles(user);

            if (wallet.CurrencyId == withdrawalDto.CurrencyId)
            {
                if (!CanWithdrawFromWallet(wallet.Balance, withdrawalDto.Amount))
                {
                    return false;
                }
                wallet.Balance -= withdrawalDto.Amount;

                _transactionRepository.CreateTransaction(TransactionType.Debit, withdrawalDto.Amount, withdrawalDto.WalletId,
                    withdrawalDto.CurrencyId);
            }
            else
            {
                var targetCode = _currencyRepository.GetCurrencyCode(wallet.CurrencyId);
                var sourceCode = _currencyRepository.GetCurrencyCode(withdrawalDto.CurrencyId);

                var newAmount = await CurrencyRate.ConvertCurrency(sourceCode, targetCode, withdrawalDto.Amount);

                if (!CanWithdrawFromWallet(wallet.Balance, newAmount))
                {
                    return false;
                }

                wallet.Balance -= newAmount ?? 0;

                _transactionRepository.CreateTransaction(TransactionType.Debit, newAmount ?? 0, withdrawalDto.WalletId, withdrawalDto.CurrencyId);
            }

            return await UpdateWallet(wallet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldWallet"></param>
        /// <param name="newWallet"></param>
        /// <returns></returns>
        public async Task<bool> ChangeMainCurrency(Wallet oldWallet, Wallet newWallet)
        {
            oldWallet.IsMain = false;
            newWallet.IsMain = true;

            return await UpdateWallet(oldWallet) && await UpdateWallet(newWallet);
        }
    }
}