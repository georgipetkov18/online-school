namespace OnlineSchoolApi.InputModels
{
    public class StudentInputModel
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Guid? ClassId { get; set; }
    }
}
