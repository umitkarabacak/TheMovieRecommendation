using Application.Exceptions;
using Application.Models;
using Application.Movies.Commands.CreateMovieRecommendation;
using Application.Movies.Commands.CreateMovieVote;
using Application.Movies.Queries.GetMovieDetail;
using Application.Movies.Queries.GetMoviesListWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        // POST api/v1/[controller]/{movieId}/movie-vote
        [HttpPost]
        [Route("{movieId}/movie-vote")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MovieVoteAsync(Guid movieId, CreateMovieVoteCommand createMovieVoteCommand)
        {
            if (movieId != createMovieVoteCommand.MovieId)
                return BadRequest("key values entered do not match each other. 'MovieId route & bind object'");

            var response = await Mediator.Send(createMovieVoteCommand);

            return Ok(response);
        }

        // POST api/v1/[controller]/{movieId}/movie-recommendation
        [HttpPost]
        [Route("{movieId}/movie-recommendation")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MovieRecommendationAsync(int movieId, CreateMovieRecommendationCommand createMovieRecommendationCommand)
        {
            if (movieId != createMovieRecommendationCommand.MovieId)
                return BadRequest("key values entered do not match each other. 'MovieId route & bind object'");

            await Mediator.Send(createMovieRecommendationCommand);

            return NoContent();
        }
    }
}
