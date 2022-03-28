using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class TimetableEntity : BaseEntity
    {
        [Required]
        public DayOfWeek Day { get; set; }

        public Guid SubjectId { get; set; }
        public virtual SubjectEntity Subject { get; set; } = null!;

        public Guid LessonId { get; set; }
        public virtual LessonEntity Lesson { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public virtual TeacherEntity Teacher { get; set; } = null!;

        public Guid ClassId { get; set; }
        public virtual ClassEntity Class { get; set; } = null!;
    }
}
