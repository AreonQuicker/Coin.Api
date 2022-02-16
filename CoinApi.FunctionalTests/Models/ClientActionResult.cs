using System.Collections.Generic;
using E.S.Api.Helpers.Enums;

namespace CoinApi.FunctionalTests.Models
{
    public class ClientActionResult<T>
    {
        public T Data { get; set; }
        public ApiResultTypeEnum ResultType { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public string ErrorCode { get; set; }
        public bool Failed { get; set; }
        public string InternalErrorCode { get; set; }
    }

    public class ClientActionResult
    {
        public ApiResultTypeEnum ResultType { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public string ErrorCode { get; set; }
        public bool Failed { get; set; }
        public string InternalErrorCode { get; set; }
    }
}