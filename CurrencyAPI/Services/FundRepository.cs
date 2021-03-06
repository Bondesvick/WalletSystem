﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Data;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    /// <summary>
    ///
    /// </summary>
    public class FundRepository : IFundRepository
    {
        private readonly DataContext _context;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public FundRepository(DataContext context) => _context = context;

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Funding GetFundingById(int id) => _context.Fundings.Include(f => f.Destination).FirstOrDefault(f => f.Id == id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="fundNoobDto"></param>
        /// <param name="walletId"></param>
        /// <returns></returns>
        public async Task<bool> CreateFunding(FundNoobDto fundNoobDto, int walletId)
        {
            Funding funding = new Funding()
            {
                DestinationId = walletId,
                CurrencyId = fundNoobDto.CurrencyId,
                Amount = fundNoobDto.Amount,
                IsApproved = false
            };

            try
            {
                await _context.Fundings.AddAsync(funding);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFunding(int id)
        {
            var funding = GetFundingById(id);

            try
            {
                _context.Fundings.Remove(funding);
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
        /// <returns></returns>
        public List<Funding> GetAllFundings() => _context.Fundings.Include(f => f.Currency).ToList();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Funding> GetUnApprovedFundings() => _context.Fundings.Include(f => f.Currency).Where(f => !f.IsApproved).ToList();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Funding> GetApprovedFundings() => _context.Fundings.Include(f => f.Currency).Where(f => f.IsApproved).ToList();
    }
}