using System;

namespace CoinApi.Domain.Common.Exceptions
{
    public class ServiceException : Exception, IServiceException
    {
        public ServiceException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; }
    }
}