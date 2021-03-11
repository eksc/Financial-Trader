using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddFinanceAPIHostBuilderExtension
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
             {
                 string apiKey = ConfigurationManager.AppSettings.Get("finacialApiKey");
                 services.AddSingleton<FinancialModelingPrepHttpClientFactory>(new FinancialModelingPrepHttpClientFactory(apiKey));
             });

            return host;
        }
    }
}
