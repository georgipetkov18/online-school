using OnlineSchoolBusinessLogic.Common;
using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolApi.InputModels
{
    public class TimetableEntryInputModel
    {
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
