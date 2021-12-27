using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;

namespace Application.Users.Commands
{
    public class UserRegisterCommand : IMapFrom<User>, IRequest<Guid>
    {
        public string Username { get; set; }

        public string Password { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRegisterCommand, User>();
        }
    }
}
