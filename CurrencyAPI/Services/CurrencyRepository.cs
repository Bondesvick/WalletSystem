﻿using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using WalletSystemAPI.Data;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    /// <summary>
    ///
    /// </summary>
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataContext _context;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public CurrencyRepository(DataContext context) => _context = context;

        /// <summary>
        ///
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public string GetCurrencyCode(int currencyId) => GetCurrencyById(currencyId)?.Code ?? string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public bool CurrencyExist(int currencyId) => _context.Currencies.Any(c => c.Id == currencyId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Currency GetCurrencyById(int id) => _context.Currencies.FirstOrDefault(c => c.Id == id);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Currency> GetAllCurrencies() => _context.Currencies.ToList();

        /// <summary>
        ///
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
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