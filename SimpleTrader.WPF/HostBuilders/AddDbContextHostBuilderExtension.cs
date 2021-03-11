using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using SimpleTrader.FinancialModelingPrepAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtension
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("default");
                services.AddDbContext<SimpleTraderDbContext>(o => o.UseSqlServer(connectionString));
                services.AddSingleton<SimpleTraderDbContextFactory>(new SimpleTraderDbContextFactory(connectionString));
            });

            return host;
        }
    }
}
