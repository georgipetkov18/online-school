using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
