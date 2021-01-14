using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Brokers;
using Xunit;

namespace IntegrationTest.APIs
{
    [Collection(nameof(ApiTestCollection))]
    public class CurrencyTest
    {
        //private readonly CurrencyApiBroker currencyApiBroker;

        [Fact]
        public void ShouldFetchAllCurrency()
        {
        }
    }
}