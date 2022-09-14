using System;

namespace FaithLife.Api.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } 
        public string Email { get; set; } 

        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string Verification { get; set; } 

        public DateTime VerifiedAt { get; set; }
        
        public string PasswordResetToken { get; set; } 

        public DateTime ResetTokenExpires { get; set; }


    }
}

