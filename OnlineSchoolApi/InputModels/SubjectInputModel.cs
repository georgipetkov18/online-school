using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolApi.InputModels
{
    public class SubjectInputModel
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string Code { get; set; } = null!;
    }
}
