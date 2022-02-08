namespace OnlineSchoolApi.RequestModels
{
    public class LessonInputModel
    {
        public Guid? Id { get; set;}
        public TimeSpan From { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
