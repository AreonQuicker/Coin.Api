using System;
using CoinApi.Domain.Common.Enums;

namespace CoinApi.Domain.Common.Models
{
    public class SortInfo
    {
        public SortOrderTypeEnum SortOrderType { get; }
        public string SoryKey { get; }

        public SortInfo(string soryKey, SortOrderTypeEnum sortOrderType = SortOrderTypeEnum.ASC)
        {
            SoryKey = soryKey;
            SortOrderType = sortOrderType;
        }
    }
}