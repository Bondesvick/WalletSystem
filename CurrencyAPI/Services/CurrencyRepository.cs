using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using WalletSystemAPI.Data;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataContext _context;

        public CurrencyRepository(DataContext context)
        {
            _context = context;
        }

        public string GetCurrencyCode(int currencyId)
        {
            var currency = GetCurrencyById(currencyId);

            return currency?.Code ?? string.Empty;
        }

        public bool CurrencyExist(int currencyId)
        {
            return _context.Currencies.Any(c => c.Id == currencyId);
        }

        public Currency GetCurrencyById(int id)
        {
            return _context.Currencies.FirstOrDefault(c => c.Id == id);
        }

        public bool AddCurrency(Currency currency)
        {
            try
            {
                _context.Currencies.AddAsync(currency);
                _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCurrency(int id)
        {
            var currency = GetCurrencyById(id);
            try
            {
                _context.Currencies.Remove(currency);
                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCurrency(Currency currency)
        {
            try
            {
                _context.Currencies.Update(currency);
                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}