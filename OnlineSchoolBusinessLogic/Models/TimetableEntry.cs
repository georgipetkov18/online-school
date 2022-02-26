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

    [AttributeUsage(AttributeTargets.Property)]
    public class DayOfWeekAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valueString = value?.ToString()!;
            var capitalized = char.ToUpper(valueString[0]) + valueString.Substring(1).ToLower();
            return Enum.TryParse(capitalized, out DayOfWeek _);
        }
    }
}
