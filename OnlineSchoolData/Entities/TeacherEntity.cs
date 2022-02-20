using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class TeacherEntity : BaseEntity
    {
        [Required]
        public virtual SubjectEntity Subject { get; set; } = null!;

        [Required]
        public virtual UserEntity User { get; set; } = null!;

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();
    }
}
