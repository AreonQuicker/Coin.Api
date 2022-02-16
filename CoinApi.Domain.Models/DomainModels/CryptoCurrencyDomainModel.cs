using System;
using System.Collections.Generic;

namespace CoinApi.Domain.Models.DomainModels
{
    public class CryptoCurrencyDomainModel : AuditDomainModel
    {
        public CryptoCurrencyDomainModel()
        {
            Quotes = new HashSet<CryptoCurrencyQuoteDomainModel>();
        }

        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Slug { get; set; }

        public decimal CmcRank { get; set; }

        public decimal NumMarketPairs { get; set; }

        public DateTime LastUpdated { get; set; }

        public ICollection<CryptoCurrencyQuoteDomainModel> Quotes { get; set; }
    }
}