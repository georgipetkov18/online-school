namespace OnlineSchoolApi.ResponseModels
{
    public class TimetableEntryResponse
    {
        public Guid TimetableEntryId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public TeacherResponse Teacher { get; set; } = null!;
        public string Class { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
