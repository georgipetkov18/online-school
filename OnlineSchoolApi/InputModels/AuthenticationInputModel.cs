namespace OnlineSchoolApi.InputModels
{
    public class AuthenticationInputModel
    {
        public string UsernameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
