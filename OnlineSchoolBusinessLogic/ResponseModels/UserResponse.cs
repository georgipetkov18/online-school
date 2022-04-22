using OnlineSchoolBusinessLogic.Common;

namespace OnlineSchoolBusinessLogic.ResponseModels
{
    public class UserResponse
    {
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public AccountStatus Status { get; set; }
    }
}
