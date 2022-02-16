using System;
using E.S.Api.Helpers.Exceptions;

namespace CoinApi.Domain.Common.Exceptions
{
    public class NotFoundException : ApiNotFoundException, IServiceException
    {
        public NotFoundException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public NotFoundException(string message, Exception innerException, string errorCode) : base(message,
            innerException)
        {
            ErrorCode = errorCode;
        }

        public NotFoundException(string name, object key, string errorCode) : base(name, key)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; }
    }
}