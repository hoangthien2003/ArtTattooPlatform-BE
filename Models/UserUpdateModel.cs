﻿namespace back_end.Models
{
    public class UserUpdateModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Image { get; set; }
    }
}