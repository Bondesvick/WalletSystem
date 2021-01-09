using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}