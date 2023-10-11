﻿using back_end.Models;

namespace back_end.Utils
{
    public class Utils
    {
        public static async Task<string> UploadGetURLImageAsync(IFormFile image)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Assets",
                    image.FileName);
            using (var stream = System.IO.File.Create(path))
            {
                await image.CopyToAsync(stream);
            }
            return "http://35.240.234.172:8181/Assets/" + image.FileName;
        }

        public static string HashSaltPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedNewPassword;
        }
    }
}