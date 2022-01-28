using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Models
{
    internal class Lesson : BaseEntity
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Code { get; set; }

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();
    }
}
