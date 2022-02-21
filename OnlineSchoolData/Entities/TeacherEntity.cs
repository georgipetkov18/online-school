using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class TeacherEntity : BaseEntity
    {
        [Required]
        public Guid SubjectId { get; set; }
        public virtual SubjectEntity Subject { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; } = null!;

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();
    }
}
