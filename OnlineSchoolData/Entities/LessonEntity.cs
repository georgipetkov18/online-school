using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class LessonEntity : BaseEntity
    {
        [Required]
        [Range(typeof(TimeSpan), "07:30", "19:00")]
        public TimeSpan From { get; set; }

        [Required]
        [Range(5, 100)]
        public int DurationInMinutes { get; set; }

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();

    }
}
