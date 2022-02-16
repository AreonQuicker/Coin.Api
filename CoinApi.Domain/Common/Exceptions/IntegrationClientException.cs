using System;
using System.Net;

namespace CoinApi.Domain.Common.Exceptions
{
    public class IntegrationClientException : Exception, IServiceException
    {
        public IntegrationClientException(string message, string internalErrorMessage, HttpStatusCode? statusCode,
            int internalErrorCode, string errorCode) : base(message)
        {
            InternalErrorMessage = internalErrorMessage;
            StatusCode = statusCode;
            InternalErrorCode = internalErrorCode;
            ErrorCode = errorCode;
        }

        public IntegrationClientException(string message, HttpStatusCode? statusCode, string errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public string InternalErrorMessage { get; }
        public HttpStatusCode? StatusCode { get; }
        public int? InternalErrorCode { get; }
        public string ErrorCode { get; }
    }
}