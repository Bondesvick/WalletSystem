using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletSystemAPI.Data;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class FundRepository : IFundRepository
    {
        private readonly DataContext _context;

        public FundRepository(DataContext context)
        {
            _context = context;
        }

        public Funding GetFundingById(int id)
        {
            return _context.Fundings.Include(f => f.Destination).FirstOrDefault(f => f.Id == id);
        }

        public async Task<bool> CreateFunding(FundingDto fundingDto)
        {
            Funding funding = new Funding()
            {
                DestinationId = fundingDto.WalletId,
                CurrencyId = fundingDto.CurrencyId,
                Amount = fundingDto.Amount,
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

        public List<Funding> GetAllFundings()
        {
            return _context.Fundings.Include(f => f.Currency).ToList();
        }

        public List<Funding> GetUnApprovedFundings()
        {
            return _context.Fundings.Include(f => f.Currency).Where(f => !f.IsApproved).ToList();
        }

        public List<Funding> GetApprovedFundings()
        {
            return _context.Fundings.Include(f => f.Currency).Where(f => f.IsApproved).ToList();
        }
    }
}