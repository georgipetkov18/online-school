namespace OnlineSchoolBusinessLogic.Models
{
    public class Student : User
    { 
        public Guid? ClassId { get; set; }
    }
}
