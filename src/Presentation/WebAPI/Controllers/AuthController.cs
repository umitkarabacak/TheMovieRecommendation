using Application.Users.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        public AuthController(ILogger<AuthController> logger)
            : base(logger)
        {

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterCommand userRegisterCommand)
        {
            var response = await Mediator.Send(userRegisterCommand);

            return Ok(response);
        }
    }
}
