namespace OnlineSchoolBusinessLogic.Models
{
    public class TimetableEntriesInfo
    {
        public TimeSpan SendInfoAfter { get; set; }
        public TimetableEntry? Current { get; set; }
        public TimetableEntry? Next { get; set; }
    }
}
