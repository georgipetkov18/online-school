using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class ClassEntity : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public virtual ICollection<StudentEntity> Students { get; set; } = new HashSet<StudentEntity>();

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();
    }
}
