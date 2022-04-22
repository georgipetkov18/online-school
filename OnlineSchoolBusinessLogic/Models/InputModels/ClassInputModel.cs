using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.InputModels
{
    public class ClassInputModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
    }
}
