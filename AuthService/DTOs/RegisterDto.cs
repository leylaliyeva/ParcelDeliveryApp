﻿namespace AuthService
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; } 
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
