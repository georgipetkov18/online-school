namespace OnlineSchoolBusinessLogic.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public TimeSpan From { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
