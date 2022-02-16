using System;
using E.S.Api.Helpers.Exceptions;

namespace CoinApi.Domain.Common.Exceptions
{
    public class AlreadyExistException : ApiAlreadyExistException, IServiceException
    {
        public AlreadyExistException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public AlreadyExistException(string message, Exception innerException, string errorCode) : base(message,
            innerException)
        {
            ErrorCode = errorCode;
        }

        public AlreadyExistException(string name, object key, string errorCode) : base(name, key)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; }
    }
}