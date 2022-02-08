namespace OnlineSchoolBusinessLogic.Models
{
    public class Student : User
    { 
        public Class? Class { get; set; } = null!;
    }
}
