using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Integration.Core.Responses;
using CoinApi.Integration.Services;
using E.S.ApiClientHandler.Interfaces;
using E.S.ApiClientHandler.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoinApi.UnitTests.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public async Task GetCoins_ShouldReturnResult_WithValidQueury()
        {
            var expected =
                new ApiResponse<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>>(
                    new IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>(), true, null)
                {
                    Value = new IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>
                    {
                        Data = new List<CoinIntegrationClientResponse>()
                    }
                };

            var apiCachingManager = new Mock<IApiCachingManager>();
            var apiRequestBuilder = MockApiRequestBuilder(expected);

            var coinIntegrationClientService =
                new CoinIntegrationClientServiceBase(apiRequestBuilder.Object, apiCachingManager.Object);

            var results = await coinIntegrationClientService.GetCoinsAsync();

            results.Should().NotBeNull();
            results.Data.Should().NotBeNull();
            results.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCoins_ShouldThrowException_WithInValidQueury()
        {
            var expected =
                new ApiResponse<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>>(
                    new IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>(), false, null)
                {
                    Value = new IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>
                    {
                        Data = new List<CoinIntegrationClientResponse>()
                    }
                };

            var apiCachingManager = new Mock<IApiCachingManager>();
            var apiRequestBuilder = MockApiRequestBuilder(expected);

            var coinIntegrationClientService =
                new CoinIntegrationClientServiceBase(apiRequestBuilder.Object, apiCachingManager.Object);

            Func<Task> f = async () => { await coinIntegrationClientService.GetCoinsAsync(); };

            await f.Should().ThrowAsync<IntegrationClientException>();
        }

        [Fact]
        public async Task GetCoins_ShouldThrowExceptionWithErrorCode_WithInValidQueury()
        {
            var expected =
                new ApiResponse<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>>(
                    new IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>(), false, null)
                {
                    Value = new IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>
                    {
                        Data = new List<CoinIntegrationClientResponse>(),
                        Status = new StatusIntegrationClientResponse
                        {
                            ErrorCode = 1
                        }
                    }
                };

            var apiCachingManager = new Mock<IApiCachingManager>();
            var apiRequestBuilder = MockApiRequestBuilder(expected);

            var coinIntegrationClientService =
                new CoinIntegrationClientServiceBase(apiRequestBuilder.Object, apiCachingManager.Object);

            Func<Task> f = async () => { await coinIntegrationClientService.GetCoinsAsync(); };

            await f.Should().ThrowAsync<IntegrationClientException>().Where(w => w.InternalErrorCode == 1);
        }

        [Fact]
        public async Task GetCurrencies_ShouldReturnResult_WithValidQueury()
        {
            var expected =
                new ApiResponse<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>>(
                    new IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>(), true, null)
                {
                    Value = new IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>
                    {
                        Data = new List<CurrencyIntegrationClientResponse>()
                    }
                };

            var apiCachingManager = new Mock<IApiCachingManager>();
            var apiRequestBuilder = MockApiRequestBuilder(expected);

            var coinIntegrationClientService =
                new CoinIntegrationClientServiceBase(apiRequestBuilder.Object, apiCachingManager.Object);

            var results = await coinIntegrationClientService.GetCurrenciesAsync();

            results.Should().NotBeNull();
            results.Data.Should().NotBeNull();
            results.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCurrencies_ShouldThrowException_WithInValidQueury()
        {
            var expected =
                new ApiResponse<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>>(
                    new IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>(), false, null)
                {
                    Value = new IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>
                    {
                        Data = new List<CurrencyIntegrationClientResponse>()
                    }
                };

            var apiCachingManager = new Mock<IApiCachingManager>();
            var apiRequestBuilder = MockApiRequestBuilder(expected);

            var coinIntegrationClientService =
                new CoinIntegrationClientServiceBase(apiRequestBuilder.Object, apiCachingManager.Object);

            Func<Task> f = async () => { await coinIntegrationClientService.GetCurrenciesAsync(); };

            await f.Should().ThrowAsync<IntegrationClientException>();
        }

        [Fact]
        public async Task GetCurrencies_ShouldThrowExceptionWithErrorCode_WithInValidQueury()
        {
            var expected =
                new ApiResponse<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>>(
                    new IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>(), false, null)
                {
                    Value = new IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>
                    {
                        Data = new List<CurrencyIntegrationClientResponse>(),
                        Status = new StatusIntegrationClientResponse
                        {
                            ErrorCode = 1
                        }
                    }
                };

            var apiCachingManager = new Mock<IApiCachingManager>();
            var apiRequestBuilder = MockApiRequestBuilder(expected);

            var coinIntegrationClientService =
                new CoinIntegrationClientServiceBase(apiRequestBuilder.Object, apiCachingManager.Object);

            Func<Task> f = async () => { await coinIntegrationClientService.GetCurrenciesAsync(); };

            await f.Should().ThrowAsync<IntegrationClientException>().Where(w => w.InternalErrorCode == 1);
        }

        #region Private Methods

        private Mock<IApiRequestBuilder> MockApiRequestBuilder<T>(
            ApiResponse<IntegrationClientResponseBase<IList<T>>> expected)
        {
            var apiRequestBuilder = new Mock<IApiRequestBuilder>();
            var apiRequestBuilderInner1 = new Mock<IApiRequestBuilderInner1>();
            var apiRequestBuilderInner1_2 = new Mock<IApiRequestBuilderInner1>();
            var apiRequestBuilderInner2 = new Mock<IApiRequestBuilderInner2>();

            apiRequestBuilderInner2
                .Setup(a => a.ExecuteAsync(
                    It.IsAny<IntegrationClientResponseBase<IList<T>>>()))
                .ReturnsAsync(() => expected);

            apiRequestBuilderInner1_2.Setup(a => a.WithUrl(It.IsAny<string>()))
                .Returns(() => apiRequestBuilderInner2.Object);

            apiRequestBuilderInner1.Setup(a => a.WithCacheClient(It.IsAny<IApiCachingManager>(), It.IsAny<int>()))
                .Returns(() => apiRequestBuilderInner1_2.Object);

            apiRequestBuilder.Setup(a => a.New()).Returns(() => apiRequestBuilderInner1.Object);

            return apiRequestBuilder;
        }

        #endregion
    }
}