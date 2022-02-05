namespace OnlineSchoolBusinessLogic.Models
{
    public record Lesson
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Code { get; init; }
    }
}
