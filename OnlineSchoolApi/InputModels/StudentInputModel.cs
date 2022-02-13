using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolApi.InputModels
{
    public class StudentInputModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public Guid ClassId { get; set; }
    }
}
