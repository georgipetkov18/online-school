using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public record Lesson
    {
        public Guid Id { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "07:30", "19:00")]
        public TimeSpan From { get; set; }

        [Required]
        [Range(5, 100)]
        public int DurationInMinutes { get; set; }
    }
}
