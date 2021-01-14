using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using WalletSystemAPI;
using WalletSystemAPI.Models;

namespace IntegrationTest.Brokers
{
    public partial class CurrencyApiBroker
    {
        protected readonly HttpClient TestClient;
        protected readonly WebApplicationFactory<Startup> WebApplicationFactory;
        protected readonly IRESTFulApiFactoryClient ApiFactoryClient;

        protected CurrencyApiBroker()
        {
            this.WebApplicationFactory = new WebApplicationFactory<Startup>();
            this.TestClient = this.WebApplicationFactory.CreateClient();
            this.ApiFactoryClient = new RESTFulApiFactoryClient(this.TestClient);
        }

        //private const string url = "api/currency";
    }
}