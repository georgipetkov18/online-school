using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class AdministratorEntity : BaseEntity
    {
        [Required]
        public virtual UserEntity User { get; set; } = null!;
    }
}
