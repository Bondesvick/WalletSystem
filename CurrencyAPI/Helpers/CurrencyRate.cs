using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WalletSystemAPI.Dtos;

namespace WalletSystemAPI.Helpers
{
    public static class CurrencyRate
    {
        public static async Task<RequestRoot> GetExchangeRate()
        {
            using HttpClient client = new HttpClient();

            using HttpResponseMessage response = await client.GetAsync("http://data.fixer.io/api/latest?access_key=c9d6219ffc703c54084500873bf7b73e&symbols");

            if (response.IsSuccessStatusCode)
            {
                using HttpContent content = response.Content;
                var myContent = await content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<RequestRoot>(myContent);

                return value;
            }

            return null;
        }

        public static async Task<decimal> ConvertCurrency(string sourceCode, string targetCode, decimal amount)
        {
            var rate = await GetExchangeRate();

            var baseRate = ReflectionConverter.GetPropertyValue(rate.Rates, rate.Base);
            var sourceRate = ReflectionConverter.GetPropertyValue(rate.Rates, sourceCode);
            var targetRate = ReflectionConverter.GetPropertyValue(rate.Rates, targetCode);

            decimal.TryParse(sourceRate.ToString(), out decimal sourceRateValue);
            decimal.TryParse(targetRate.ToString(), out decimal targetRateValue);
            decimal.TryParse(baseRate.ToString(), out decimal baseRateValue);

            // First Convert the source currency amoun to its equivalent in base currency
            var sourceToBaseValue = (baseRateValue * amount) / sourceRateValue;

            // Then Convert to the target currency
            var converted = (sourceToBaseValue * targetRateValue) / baseRateValue;

            return converted;
        }
    }
}