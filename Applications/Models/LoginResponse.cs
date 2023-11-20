﻿using Domain.Enum;

namespace Applications.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string RefreshToken { get; set; }
    }
}
