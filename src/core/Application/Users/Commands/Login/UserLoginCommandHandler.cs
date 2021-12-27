using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Login
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, string>
    {
        private readonly IProjectContext _projectContext;

        public UserLoginCommandHandler(IProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _projectContext.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(request.Username)
                                       && u.Password.Equals(request.Password)
                    , cancellationToken);

            if (user is null)
                throw new Exception($"The username or password invalid");

            // TODO : change response with JWT token
            return user.Id.ToString();
        }
    }
}
