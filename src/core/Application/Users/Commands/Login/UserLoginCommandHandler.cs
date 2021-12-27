using Application.Interfaces;
using Application.Users.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Login
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserResponse>
    {
        private readonly IProjectContext _projectContext;
        private readonly IMapper _mapper;
        private readonly static string SECRET_KEY = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";

        public UserLoginCommandHandler(IProjectContext projectContext
            , IMapper mapper)
        {
            _projectContext = projectContext;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _projectContext.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(request.Username)
                                       && u.Password.Equals(request.Password)
                    , cancellationToken);

            if (user is null)
                throw new Exception($"The username or password invalid");

            var userResponse = _mapper.Map<UserResponse>(user);
                userResponse.TokenCode = generateJwtToken(userResponse);

            return userResponse;
        }


        private string generateJwtToken(UserResponse userResponse)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var scretBytes = Encoding.ASCII.GetBytes(SECRET_KEY);
            var key = new SymmetricSecurityKey(scretBytes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "https://localhost:5001",
                Issuer = "https://localhost:5001",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userResponse.Username),
                    new Claim(ClaimTypes.Name, userResponse.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
