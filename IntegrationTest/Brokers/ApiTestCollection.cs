using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTest.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    internal class ApiTestCollection : ICollectionFixture<CurrencyApiBroker>
    {
    }
}