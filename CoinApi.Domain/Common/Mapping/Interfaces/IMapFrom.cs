using AutoMapper;

namespace CoinApi.Domain.Common.Mapping.Interfaces
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType()).ReverseMap();
        }
    }
}