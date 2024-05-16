using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WpfApp1.Utilities
{
    class Validater
    {
        public static bool IsValidImageFormat(string fileUrl)
        {
            var validImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                 {
                    ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".jfif"
                 };

            string fileExtension = Path.GetExtension(fileUrl);
            return validImageExtensions.Contains(fileExtension);
        }

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
        public static Regex eMailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        public static StringBuilder Validate(Users user)
        {
            StringBuilder error = new StringBuilder();
            if (user.Name.Length <= 0)
                error.Append("Имя не может быть пустым!");
            if (user.Name.Length >= 30)
                error.Append("Длина имени не может быть больше 30!");
            if(user.Email.Length <= 0)
                error.Append("Почта не может быть пуста!");
            if (user.Name.Length >= 30)
                error.Append("Длина почты не может быть больше 30!");
            if (!eMailRegex.IsMatch(user.Email))
                error.Append("Почта неверно введена!");
            return error;
        }
        public static StringBuilder Validate(Subscription_type type)
        {
            StringBuilder error = new StringBuilder();
            if (type.Name.Length <= 0)
                error.Append("Имя должно быть заполнено!");
            if (type.Name.Length >= 30)
                error.Append("Длина имени не может быть больше 30!");

            return error;
        }
        public static StringBuilder Validate(Posts post)
        {
            StringBuilder error = new StringBuilder();
            if (post.Text.Length <= 0)
                error.Append("Текст должн быть заполнен!");
            if (post.Text.Length >= 100)
                error.Append("Текст не может быть больше 100 символов");
            if (post.Files.File_url.Length <= 0)
                error.Append("Файл должн быть выбран!");
            return error;
        }
    }
}
