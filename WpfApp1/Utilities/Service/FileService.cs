using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Models
{
    internal class FileService
    {
        public static void DeleteFolder(string folderName)
        {
            string relativePath = Path.Combine("Resources/Data/Files", folderName);
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (Directory.Exists(fullPath))
            {
                try
                {
                    Directory.Delete(fullPath, true);

                }
                catch(Exception e)
                {

                }
            }
        }
        public static void DownloadFileToFolder(string folderName, string fileName, string path)
        {
            try
            {
                string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "Files", folderName, fileName);

                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show("Исходный файл не найден.");
                    return;
                }

                string destinationPath = Path.Combine(path, fileName);
                File.Copy(sourcePath, destinationPath, true);
                MessageBox.Show($"Файл успешно скачан");
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Произошла ошибка при копировании файла: {ex.Message}");
            }
        }
        public static void CopyFileToFolder(string sourceFilePath, string folderName)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    return;
                }

                string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "Files", folderName);

                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }

                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(destinationPath, fileName);

                File.Copy(sourceFilePath, destinationFilePath, true);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Произошла ошибка при копировании файла: {ex.Message}");
            }
        }

    }
}
