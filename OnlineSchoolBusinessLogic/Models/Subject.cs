using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public record Subject
    {
        public Guid Id { get; init; }

        [Required]
        [MaxLength(40)]
        public string Name { get; init; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Code { get; init; } = string.Empty;
    }
}
