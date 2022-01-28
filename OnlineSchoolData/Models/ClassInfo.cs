using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Models
{
    internal class ClassInfo : BaseEntity
    {
        [Required]
        public TimeSpan From { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();

    }
}
