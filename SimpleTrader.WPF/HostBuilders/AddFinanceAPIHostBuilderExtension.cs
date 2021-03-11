using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI;
using SimpleTrader.FinancialModelingPrepAPI.Models;
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
                 services.AddSingleton(new FinancialModelingPrepAPIKey(apiKey));

                 services.AddHttpClient<FinancialModelingPrepHttpClient>(c =>
                 {
                     c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                 });
             });

            return host;
        }
    }
}
