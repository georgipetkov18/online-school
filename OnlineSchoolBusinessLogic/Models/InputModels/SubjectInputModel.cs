using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.InputModels
{
    public class SubjectInputModel
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Code { get; set; } = null!;
    }
}
