namespace OnlineSchoolBusinessLogic.Models
{
    public record AuthenticateModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateModel(string username, string email, string role, string jwtToken, string refreshToken)
        {
            this.Username = username;
            this.Email = email;
            this.Role = role;
            this.JwtToken = jwtToken;
            this.RefreshToken = refreshToken;
        }
    }
}
