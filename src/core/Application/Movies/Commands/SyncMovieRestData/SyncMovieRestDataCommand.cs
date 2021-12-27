using MediatR;

namespace Application.Movies.Commands.SyncMovieRestData
{
    public class SyncMovieRestDataCommand : IRequest
    {
        public string ApiKey { get; set; }

        public string ApiMovieUrl { get; set; }

        public string ApiGenreUrl { get; set; }

        public int SyncFrequencyHour { get; set; }

        public int TakeMovieRowCount { get; set; }
    }
}
