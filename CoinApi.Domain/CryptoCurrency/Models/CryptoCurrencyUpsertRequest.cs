using System;
using System.Collections.Generic;
using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.CryptoCurrency.Models
{
    public class CryptoCurrencyUpsertRequest : IMapFrom<CryptoCurrencyDomainModel>
    {
        public string Symbol { get; set; }

        public string Slug { get; set; }

        public decimal CmcRank { get; set; }

        public decimal NumMarketPairs { get; set; }


        public DateTime LastUpdated { get; set; }

        public List<CryptoCurrencyQuoteDomainModel> Quotes { get; set; }
    }
}