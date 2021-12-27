using Application.Common.Mappings;
using Application.Users.ViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.Login
{
    public class UserLoginCommand : IMapFrom<User>, IRequest<UserResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserLoginCommand, User>();
        }
    }
}
