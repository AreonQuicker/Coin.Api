using E.S.Api.Helpers.Results;

namespace CoinApi.Models
{
    /// <summary>
    /// All results will be wrapped in a Api result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiActionResult<T>
    {
        public ApiResult(T data, string message = null, params string[] errors) : base(data, message, errors)
        {
        }

        public ApiResult(T data, string errorCode, string internalErrorCode, string message = null,
            params string[] errors) : base(data, message, errors)
        {
            ErrorCode = errorCode;
            InternalErrorCode = internalErrorCode;
        }

        public ApiResult(T data, string errorCode, string internalErrorCode, params string[] errors) : base(data,
            errors)
        {
            ErrorCode = errorCode;
            InternalErrorCode = internalErrorCode;
        }

        public ApiResult(T data, string errorCode, string internalErrorCode) : base(data)
        {
            ErrorCode = errorCode;
            InternalErrorCode = internalErrorCode;
        }

        public string ErrorCode { get; set; }
        public string InternalErrorCode { get; set; }
    }

    public class ApiResult : ApiActionResult
    {
        public ApiResult(string errorCode, string internalErrorCode, string message = null, params string[] errors) :
            base(message, errors)
        {
            ErrorCode = errorCode;
            InternalErrorCode = internalErrorCode;
        }

        public ApiResult(string errorCode, string internalErrorCode, params string[] errors) : base(errors)
        {
            ErrorCode = errorCode;
            InternalErrorCode = internalErrorCode;
        }

        public string ErrorCode { get; set; }
        public string InternalErrorCode { get; set; }
    }
}