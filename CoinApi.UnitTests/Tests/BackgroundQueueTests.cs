using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CoinApi.Background.Queue;
using CoinApi.Background.Queue.Core.Enums;
using CoinApi.Background.Queue.Core.Requests;
using FluentAssertions;
using Xunit;

namespace CoinApi.UnitTests.Tests
{
    public class BackgroundQueueTests
    {
        [Theory]
        [InlineAutoData(BackgroundQueueRequestTypeEnum.SyncCryptoCurrency)]
        [InlineAutoData(BackgroundQueueRequestTypeEnum.SyncCoinsInBackground)]
        [InlineAutoData(BackgroundQueueRequestTypeEnum.SyncCryptoCurrenciesInBackground)]
        public async Task QueueBackgroundRequest_ShouldHaveRequest_WithValidType(BackgroundQueueRequestTypeEnum type,
            IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var queue = fixture.Create<BackgroundQueue>();

            var request = new BackgroundQueueRequest(type);

            await queue.QueueAsync(request);

            var result = await queue.DequeueAsync(CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().Be(request);
            result.Type.Should().Be(type);
        }

        [Theory]
        [InlineAutoData(1)]
        public async Task QueueBackgroundRequest_ShouldHaveNumberOfRequests_WitTotal(int total, IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());

            var queue = fixture.Create<BackgroundQueue>();

            var request = new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCryptoCurrency);

            for (var i = 0; i < total; i++) await queue.QueueAsync(request);

            var totalResult = 0;

            for (var i = 0; i < total; i++)
            {
                var result = await queue.DequeueAsync(CancellationToken.None);
                if (result != null)
                    totalResult++;
            }


            totalResult.Should().Be(total);
        }
    }
}