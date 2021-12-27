using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.ViewModels
{
    public class UserResponse : IMapFrom<User>
    {
        public string Username { get; set; }

        public string TokenCode { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserResponse>();
        }
    }
}
