using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class StudentEntity : BaseEntity
    {
        [Required]
        public virtual ClassEntity Class { get; set; } = null!;

        public Guid ClassId { get; set; }

        [Required]
        public virtual UserEntity User { get; set; } = null!;
    }
}
