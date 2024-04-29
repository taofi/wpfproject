using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.IdentityModel.Protocols.WSTrust;
using System.Runtime.Remoting.Contexts;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users _user;
        public UserPage()
        {
            _user = Store.User;
            InitializeComponent();
            DataContext = _user;
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                _user.UserAvatar = selectedFileName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(_user.UserAvatar));
                UserIcon.Source = bitmapImage;
            }
        }
        static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_user.Name))
                errors.AppendLine("Укажите имя!");
            else if (_user.Name.Length > 30)
                errors.AppendLine("Размер имени должен быть не больше 30");


            if (string.IsNullOrWhiteSpace(_user.Email))
                errors.AppendLine("Укажите почту!");
            else
            if (!ValidateEmail(_user.Email))
                errors.AppendLine("Почта указана не верно" + _user.Email + "a");
            else if (_user.Email.Length > 40)
                errors.AppendLine("Размер почты должен быть не больше 40");

            if (string.IsNullOrWhiteSpace(_user.Logins.Password))
                errors.AppendLine("Укажите пароль!");
            else if (_user.Logins.Password.Length > 30)
                errors.AppendLine("Размер пороля должен быть не больше 30");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                BaseModel.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " id: " + _user.User_id);
            }
        }
    }
}
