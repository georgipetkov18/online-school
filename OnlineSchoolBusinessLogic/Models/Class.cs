using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public record Class
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public ICollection<Guid>? Students { get; set; } = new HashSet<Guid>();
    }
}
