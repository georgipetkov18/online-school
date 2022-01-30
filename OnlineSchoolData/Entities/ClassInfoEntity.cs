using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class ClassInfoEntity : BaseEntity
    {
        [Required]
        public TimeSpan From { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();

    }
}
