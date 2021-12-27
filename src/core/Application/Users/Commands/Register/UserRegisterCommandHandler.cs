using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Register
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Guid>
    {
        private readonly IProjectContext _projectContext;
        private readonly IMapper _mapper;

        public UserRegisterCommandHandler(IProjectContext projectContext
            , IMapper mapper)
        {
            _projectContext = projectContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var anyUser = await _projectContext.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(request.Username), cancellationToken);

            if (anyUser is not null)
                throw new Exception($"The username is used by another user. Requested User Name is :  \t'{request.Username}'");

            var user = _mapper.Map<User>(request);

            await _projectContext.Users.AddAsync(user, cancellationToken);
            await _projectContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
