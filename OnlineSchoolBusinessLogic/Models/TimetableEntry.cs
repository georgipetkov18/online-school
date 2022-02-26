using OnlineSchoolBusinessLogic.Common;
using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public class TimetableEntry
    {
        public Guid Id { get; set; }

        [Required]
        [DayOfWeek]
        public string DayOfWeek { get; set; } = null!;

        public Subject Subject { get; set; } = null!;
        public Lesson Lesson { get; set; } = null!;
        public Class Class { get; set; } = null!;
        public Guid TeacherId { get; set; }
    } 
}
