using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public record Subject
    {
        public Guid Id { get; init; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; init; } = string.Empty;

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Code { get; init; } = string.Empty;
    }
}
