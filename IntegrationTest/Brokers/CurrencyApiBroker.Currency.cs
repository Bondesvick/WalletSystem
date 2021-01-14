using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Models;
using WalletSystemAPI.Models;

namespace IntegrationTest.Brokers
{
    public partial class CurrencyApiBroker
    {
        private const string url = "api/currency";

        public async ValueTask<GetCurrencyDto> PostCurrencyAsync(GetCurrencyDto currency) =>
            await ApiFactoryClient.PostContentAsync(url, currency);
    }
}