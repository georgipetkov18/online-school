using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolApi.InputModels
{
    public class LessonInputModel
    {
        [Required]
        [Range(typeof(TimeSpan), "07:30", "19:00")]
        public TimeSpan From { get; set; }

        [Required]
        [Range(5, 100)]
        public int DurationInMinutes { get; set; }
    }
}
