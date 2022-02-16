namespace CoinApi.Domain.Common.Exceptions
{
    public interface IServiceException
    {
        string ErrorCode { get; }
    }
}