namespace OnlineSchoolBusinessLogic.Models
{
    public record Subject
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Code { get; init; }
    }
}
