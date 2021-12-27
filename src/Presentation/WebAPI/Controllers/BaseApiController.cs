using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private ISender _mediator = null!;
        public readonly ILogger Logger; // Todo encapsulation

        public BaseApiController(ILogger logger)
        {
            Logger = logger;
        }

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
