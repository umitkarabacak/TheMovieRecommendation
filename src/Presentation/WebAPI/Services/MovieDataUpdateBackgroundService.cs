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
        private readonly BackgroundTaskSettings _backgroundTaskSettings;

        public MovieDataUpdateBackgroundService(ILogger<MovieDataUpdateBackgroundService> logger
            , IOptions<BackgroundTaskSettings> options)
        {
            _logger = logger;
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


                await Task.Delay(frequencyHour, stoppingToken);
            }

            _logger.LogWarning("MovieDataUpdateBackgroundService is execute cancelled!");

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning($"MovieDataUpdateBackgroundService is Stop! {DateTime.Now}");

            return base.StopAsync(cancellationToken);
        }
    }
}
