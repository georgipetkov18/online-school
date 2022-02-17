using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class RoleEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();
    }
}
