using Microsoft.Extensions.Configuration;

namespace CoinApi.IntegrationTests
{
    public class TestBase
    {
        protected readonly TestFactory TestFactory;

        public TestBase(TestFactory testFactory)
        {
            TestFactory = testFactory;
        }

        protected IConfigurationRoot Configuration => TestFactory.Configuration;
    }
}