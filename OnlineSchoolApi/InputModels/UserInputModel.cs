using BusinessLayer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolApi.InputModels
{
    public class UserInputModel
    {
        [Required]
        [StringLength(128, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(65, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,65}$")]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; } = null!;

        [Email]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string RoleName { get; set; } = null!;

        public Guid? ClassId { get; set; }

        public Guid? SubjectId { get; set; }
    }
}
