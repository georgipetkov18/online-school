using OnlineSchoolBusinessLogic.Common;
using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.InputModels
{
    public class TimetableEntryInputModel
    {
        public Guid? TimetableEntryId { get; set; }

        [Required]
        [DayOfWeek]
        public string DayOfWeek { get; set; } = null!;

        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public Guid LessonId { get; set; }

        [Required]
        public Guid ClassId { get; set; }

        [Required]
        public Guid TeacherId { get; set; }
    }
}
