using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    internal interface ICurrencyRepository
    {
        string GetCurrencyCode(int currencyId);

        bool CurrencyExist(int currencyId);

        Currency GetCurrencyById(int id);

        bool AddCurrency(Currency currency);

        bool DeleteCurrency(int id);

        bool UpdateCurrency(Currency currency);
    }
}