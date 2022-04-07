﻿namespace OnlineSchoolApi.ResponseModels
{
    public class AuthenticateResponse
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string JwtToken { get; set; } = null!;
        public DateTimeOffset ExpiresAfter { get; set; }
    }
}
