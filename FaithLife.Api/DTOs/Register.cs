﻿using System.ComponentModel.DataAnnotations;

namespace FaithLife.Api.DTOs
{
    public class Register
    {
        [Required]
        public string  Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string  ConfirmPassword { get; set; }
    }
}
