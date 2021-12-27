using Application.Movies.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.SyncMovieRestData
{
    public class SyncMovieRestDataCommandHandler : IRequestHandler<SyncMovieRestDataCommand>
    {
        private readonly ILogger<SyncMovieRestDataCommandHandler> _logger;

        public SyncMovieRestDataCommandHandler(ILogger<SyncMovieRestDataCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(SyncMovieRestDataCommand request, CancellationToken cancellationToken)
        {
            var genres = await GetOriginGenres(request);
            var movies = await GetOriginMovies(request);

            return Unit.Value;
        }

        private async Task<List<GenreDto>> GetOriginGenres(SyncMovieRestDataCommand requestCommand)
        {
            var client = new RestClient(requestCommand.ApiGenreUrl);

            var request = new RestRequest(Method.GET);
                request.AddParameter("api_key", requestCommand.ApiKey);

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError($"{JsonSerializer.Serialize(response)}");

                throw new System.Exception("Get Genres Fail");
            }

            var genreResponse = JsonSerializer.Deserialize<GenreResponseDto>(response.Content);

            return genreResponse.Genres;
        }

        private async Task<List<MovieDto>> GetOriginMovies(SyncMovieRestDataCommand requestCommand)
        {
            var responseObject = new List<MovieDto>();
            var pageSize = 1;

            var client = new RestClient(requestCommand.ApiMovieUrl);

            while (responseObject.Count < requestCommand.TakeMovieRowCount)
            {
                var request = new RestRequest(Method.GET);
                    request.AddParameter("api_key", requestCommand.ApiKey);
                    request.AddParameter("page", pageSize);

                var response = await client.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                    _logger.LogError($"{JsonSerializer.Serialize(response)}");

                    throw new System.Exception("Get Genres Fail");
                }

                var movieResponse = JsonSerializer.Deserialize<MovieResponseDto>(response.Content);

                responseObject.AddRange(movieResponse.results);

                if (movieResponse.page <= movieResponse.total_pages)
                    pageSize++;
                else
                    break;
            }

            return responseObject;
        }
    }
}
