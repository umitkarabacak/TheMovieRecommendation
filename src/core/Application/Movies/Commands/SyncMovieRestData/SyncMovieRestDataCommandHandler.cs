using Application.Movies.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;
using RestSharp;
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
            var genres = await GetGenres(request);

            return Unit.Value;
        }

        private async Task<GenreResponseDto> GetGenres(SyncMovieRestDataCommand requestCommand)
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

            return JsonSerializer.Deserialize<GenreResponseDto>(response.Content);
        }
    }
}
