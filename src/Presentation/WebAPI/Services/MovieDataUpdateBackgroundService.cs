using Application.Movies.Commands.SyncMovieRestData;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class MovieDataUpdateBackgroundService : BackgroundService
    {
        private readonly ILogger<MovieDataUpdateBackgroundService> _logger;
        private readonly ISender _sender;
        private readonly BackgroundTaskSettings _backgroundTaskSettings;

        public MovieDataUpdateBackgroundService(ILogger<MovieDataUpdateBackgroundService> logger
            , IOptions<BackgroundTaskSettings> options
            , ISender sender)
        {
            _logger = logger;
            _sender = sender;
            _backgroundTaskSettings = options.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning($"MovieDataUpdateBackgroundService is Start! {DateTime.Now}");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning("MovieDataUpdateBackgroundService is execute!");

            while (!stoppingToken.IsCancellationRequested)
            {
                var frequencyHour = 1000 * 60 * 60 * _backgroundTaskSettings.SyncFrequencyHour;

                var backgroundJob = new SyncMovieRestDataCommand
                {
                    ApiKey = _backgroundTaskSettings.ApiKey,
                    ApiMovieUrl = _backgroundTaskSettings.ApiMovieUrl,
                    ApiGenreUrl = _backgroundTaskSettings.ApiGenreUrl,
                    TakeMovieRowCount = _backgroundTaskSettings.TakeMovieRowCount,
                };

                await _sender.Send(backgroundJob);

                await Task.Delay(frequencyHour, stoppingToken);
            }

            _logger.LogWarning("MovieDataUpdateBackgroundService is execute cancelled!");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning($"MovieDataUpdateBackgroundService is Stop! {DateTime.Now}");

            return base.StopAsync(cancellationToken);
        }
    }
}
