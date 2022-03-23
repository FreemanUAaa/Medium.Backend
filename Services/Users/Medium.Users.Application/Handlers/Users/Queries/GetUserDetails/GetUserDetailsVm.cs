using AutoMapper;
using Medium.Users.Core.Interfaces.Mapper;
using Medium.Users.Core.Models;
using System;

namespace Medium.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsVm : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Bio { get; set; }

        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, GetUserDetailsVm>()
                .ForMember(x => x.Id, 
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Email, 
                    opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.UserName, 
                    opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Bio, 
                    opt => opt.MapFrom(x => x.Bio));
        }
    }
}
