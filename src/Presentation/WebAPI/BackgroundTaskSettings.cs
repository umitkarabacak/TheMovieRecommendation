namespace WebAPI
{
    public class BackgroundTaskSettings
    {
        public string ApiKey { get; set; }

        public string ApiUrl { get; set; }

        public int SyncFrequencyHour { get; set; }

        public int TakeRowCount { get; set; }
    }
}
