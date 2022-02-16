using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CoinApi.IntegrationTests
{
    public class TestFactory : IDisposable
    {
        public IConfigurationRoot Configuration;

        public TestFactory()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
        }

        public void Dispose()
        {
        }
    }
}