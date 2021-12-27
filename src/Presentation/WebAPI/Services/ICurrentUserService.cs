using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserName != null;
        }

        public string UserName { get; }

        public bool IsAuthenticated { get; }
    }
}
