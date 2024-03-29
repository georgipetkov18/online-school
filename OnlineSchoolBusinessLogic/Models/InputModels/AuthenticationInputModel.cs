﻿using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.InputModels
{
    public class AuthenticationInputModel
    {
        [Required]
        [MaxLength(100)]
        public string UsernameOrEmail { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
