using back_end.Models;
using System;
using System.Globalization;

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

        public static DateTime ConvertToDateTime(string date)
        {
            DateTime dateTime = new DateTime();
            try
            {
                dateTime = DateTime.ParseExact(date, "MM/dd/yyyy, HH:mm tt", CultureInfo.InvariantCulture);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return dateTime;
        }
    }
}