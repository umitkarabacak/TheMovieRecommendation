using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.Login
{
    public class UserLoginCommand : IMapFrom<User>, IRequest<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserLoginCommand, User>();
        }
    }
}
