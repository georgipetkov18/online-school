namespace OnlineSchoolApi.ResponseModels
{
    public class TimetableResponse
    {
        public string SubjectName { get; set; } = null!;
        public string SubjectCode { get; set; } = null!;
        public string Teacher { get; set; } = null!;
        public string Class { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
