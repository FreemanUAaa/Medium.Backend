using AutoMapper;

namespace Medium.Users.Core.Interfaces.Mapper
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
