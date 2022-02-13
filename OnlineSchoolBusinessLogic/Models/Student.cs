using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Models
{
    public class Student : User
    { 
        [Required]
        public Guid ClassId { get; set; }
    }
}
