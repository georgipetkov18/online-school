using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public record Student : User
    { 
        [Required]
        public Guid ClassId { get; set; }
    }
}
