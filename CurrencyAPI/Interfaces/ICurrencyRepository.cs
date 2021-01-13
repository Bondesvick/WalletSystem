using System.Collections.Generic;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ICurrencyRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        string GetCurrencyCode(int currencyId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        bool CurrencyExist(int currencyId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Currency GetCurrencyById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Currency> GetAllCurrencies();

        /// <summary>
        ///
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        bool AddCurrency(Currency currency);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteCurrency(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        bool UpdateCurrency(Currency currency);
    }
}