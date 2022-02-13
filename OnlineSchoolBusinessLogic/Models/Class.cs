namespace OnlineSchoolBusinessLogic.Models
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Guid>? Students { get; set; } = new HashSet<Guid>();
    }
}
