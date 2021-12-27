using Application.Exceptions;
using Application.Models;
using Application.Movies.Queries.GetMovieDetail;
using Application.Movies.Queries.GetMoviesListWithPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        public MoviesController(ILogger<MoviesController> logger)
            : base(logger)
        {

        }

        // GET api/v1/[controller]/[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<MovieListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MoviesAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var getMoviesQuery = new GetMoviesListQuery(pageSize, pageIndex);

            var response = await Mediator.Send(getMoviesQuery);

            return Ok(response);
        }
        // GET api/v1/[controller]/{movieId}
        [HttpGet]
        [Route("{movieId}")]
        [ProducesResponseType(typeof(MovieDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MovieAsync(int movieId)
        {
            var getMovieDetailQuery = new GetMovieDetailQuery(movieId);

            var response = await Mediator.Send(getMovieDetailQuery);

            return Ok(response);
        }
    }
}
