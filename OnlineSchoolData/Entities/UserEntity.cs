using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public RoleEntity Role { get; set; } = null!;
        public ICollection<StudentEntity> Students { get; set; } = null!;
        public ICollection<TeacherEntity> Teachers { get; set; } = null!;
    }
}
