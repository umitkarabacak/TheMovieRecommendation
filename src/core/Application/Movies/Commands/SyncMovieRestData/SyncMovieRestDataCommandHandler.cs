using Application.Interfaces;
using Application.Movies.ViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProjectContext _projectContext;
        private readonly IMapper _mapper;

        public SyncMovieRestDataCommandHandler(ILogger<SyncMovieRestDataCommandHandler> logger
                , IProjectContext projectContext
                , IMapper mapper
            )
        {
            _logger = logger;
            _projectContext = projectContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SyncMovieRestDataCommand request, CancellationToken cancellationToken)
        {
            var genres = GetOriginGenres(request, cancellationToken);
            var movies = GetOriginMovies(request, cancellationToken);

            await Task.WhenAll(genres, movies);

            await BindDbContextToOriginGenres(genres.Result, cancellationToken);
            await BindDbContextToOriginMovies(movies.Result, cancellationToken);

            return Unit.Value;
        }

        private async Task<List<GenreDto>> GetOriginGenres(SyncMovieRestDataCommand requestCommand, CancellationToken cancellationToken)
        {
            var client = new RestClient(requestCommand.ApiGenreUrl);

            var request = new RestRequest(Method.GET);
            request.AddParameter("api_key", requestCommand.ApiKey);

            var response = await client.ExecuteAsync(request, cancellationToken);

            if (!response.IsSuccessful)
            {
                _logger.LogError($"{JsonSerializer.Serialize(response)}");

                throw new System.Exception("Get Genres Fail");
            }

            var genreResponse = JsonSerializer.Deserialize<GenreResponseDto>(response.Content);

            return genreResponse.Genres;
        }

        private async Task<List<MovieDto>> GetOriginMovies(SyncMovieRestDataCommand requestCommand, CancellationToken cancellationToken)
        {
            var responseObject = new List<MovieDto>();
            var pageSize = 1;

            var client = new RestClient(requestCommand.ApiMovieUrl);

            while (responseObject.Count < requestCommand.TakeMovieRowCount)
            {
                var request = new RestRequest(Method.GET);
                request.AddParameter("api_key", requestCommand.ApiKey);
                request.AddParameter("page", pageSize);

                var response = await client.ExecuteAsync(request, cancellationToken);

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

        private async Task BindDbContextToOriginGenres(List<GenreDto> genreDtos, CancellationToken cancellationToken)
        {
            var currentGenreIds = await _projectContext.Genres
                .Select(g => g.Id)
                .ToListAsync(cancellationToken);

            genreDtos = genreDtos.Where(g => !currentGenreIds.Contains(g.Id)).ToList();

            if (!genreDtos.Any())
                return;

            var genres = _mapper.Map<List<Genre>>(genreDtos);

            await _projectContext.Genres.AddRangeAsync(genres, cancellationToken);
            await _projectContext.SaveChangesAsync(cancellationToken);
        }

        private async Task BindDbContextToOriginMovies(List<MovieDto> movieDtos, CancellationToken cancellationToken)
        {
            var currentMovieIds = await _projectContext.Movies
                .Select(m => m.Id)
                .ToListAsync(cancellationToken);

            movieDtos = movieDtos.Where(m => !currentMovieIds.Contains(m.id)).ToList();

            if (!movieDtos.Any())
                return;

            var movies = _mapper.Map<List<Movie>>(movieDtos);

            await _projectContext.Movies.AddRangeAsync(movies, cancellationToken);
            await _projectContext.SaveChangesAsync(cancellationToken);
        }
    }
}
