using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class StudentEntity : UserEntity
    {
        [Required]
        public virtual ClassEntity Class { get; set; } = null!;

        public Guid ClassId { get; set; }
    }
}
