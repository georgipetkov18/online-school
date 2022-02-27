namespace OnlineSchoolApi.ResponseModels
{
    public class TimetableEntryResponse
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Teacher { get; set; } = null!;
        public string Class { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
