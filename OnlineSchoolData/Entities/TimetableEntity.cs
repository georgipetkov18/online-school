using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class TimetableEntity : BaseEntity
    {
        [Required]
        public DayOfWeek Day { get; set; }

        public virtual SubjectEntity Subject { get; set; } = null!;

        public virtual LessonEntity Lesson { get; set; } = null!;

        public virtual TeacherEntity? Teacher { get; set; } = null!;

        public virtual ClassEntity Class { get; set; } = null!;
    }
}
