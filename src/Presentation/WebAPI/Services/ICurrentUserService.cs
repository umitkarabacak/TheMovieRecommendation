using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? default;
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? default;

            IsAuthenticated = UserName != default || UserId != default;
        }

        public string UserId { get; }

        public string UserName { get; }

        public bool IsAuthenticated { get; }
    }
}
