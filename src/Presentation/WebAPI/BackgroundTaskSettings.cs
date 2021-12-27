namespace WebAPI
{
    public class BackgroundTaskSettings
    {
        public int SyncFrequencyHour { get; set; }

        public string ApiKey { get; set; }

        public string ApiGenreUrl { get; set; }

        public string ApiMovieUrl { get; set; }

        public int TakeMovieRowCount { get; set; }
    }
}
