using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class TeacherEntity : UserEntity
    {
        [Required]
        [MaxLength(70)]
        public string Subject { get; set; } = null!;

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();
    }
}
