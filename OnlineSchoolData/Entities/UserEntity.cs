using OnlineSchoolBusinessLogic.Common;
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
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public AccountStatus Status { get; set; } = AccountStatus.Pending;

        [Required]
        public RoleEntity Role { get; set; } = null!;
        public ICollection<StudentEntity> Students { get; set; } = null!;
        public ICollection<TeacherEntity> Teachers { get; set; } = null!;
        public ICollection<AdministratorEntity> Administrators { get; set; } = null!;
    }
}
