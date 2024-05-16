using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class Validater
    {
        private static readonly HashSet<string> validExtensions = new HashSet<string>
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".wmp", ".jfif"
        };
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static bool IsValidImageFormat(string path)
        {
            string extension = Path.GetExtension(path).ToLower();
            return validExtensions.Contains(extension);
        }
        public static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }

        public static StringBuilder Validate(Users user)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(user.Name))
                errors.AppendLine("Укажите имя!");
            else if (user.Name.Length > 30)
                errors.AppendLine("Размер имени должен быть не больше 30");

            if (string.IsNullOrWhiteSpace(user.Logins.Login))
                errors.AppendLine("Укажите логин!");
            else if (user.Logins.Login.Length > 20)
                errors.AppendLine("Размер логина должен быть не больше 20");

            if (string.IsNullOrWhiteSpace(user.Email))
                errors.AppendLine("Укажите почту!");
            else
            if (!ValidateEmail(user.Email))
                errors.AppendLine("Почта указана не верно");
            else if (user.Email.Length > 40)
                errors.AppendLine("Размер почты должен быть не больше 40");

            return errors;
        }

        public static StringBuilder Validate(Posts post)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(post.Text))
                errors.AppendLine("Укажите описание!");
            if (post.Text.Length > 100)
                errors.AppendLine("Описание должно быть меньше 100 символов");
            return errors;
        }
        public static StringBuilder Validate(Subscription_type post)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(post.Name))
                errors.AppendLine("Укажите название!");
            if(post.Name.Length > 30)
                errors.AppendLine("Название должно быть меньше 30 символов");

            return errors;
        }
    }
}
