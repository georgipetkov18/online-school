namespace OnlineSchoolBusinessLogic.Models
{
    public record Subject
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
    }
}
