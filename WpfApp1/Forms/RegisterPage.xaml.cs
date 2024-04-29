using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1.Forms
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private Users _newUser = new Users();
        public RegisterPage()
        {
            InitializeComponent();

            _newUser.Logins = new Logins();
            DataContext = _newUser;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            _newUser.Role = "user";
            if (string.IsNullOrWhiteSpace(_newUser.Name))
                errors.AppendLine("Укажите имя!");
            else if (_newUser.Name.Length > 30)
                errors.AppendLine("Размер имени должен быть не больше 30");

            if (string.IsNullOrWhiteSpace(_newUser.Logins.Login))
                errors.AppendLine("Укажите логин!");
            else if (_newUser.Logins.Login.Length > 20)
                errors.AppendLine("Размер логина должен быть не больше 20");
            
            if (string.IsNullOrWhiteSpace(_newUser.Email))
                errors.AppendLine("Укажите почту!");
            else
            if(!ValidateEmail(_newUser.Email))
                errors.AppendLine("Почта указана не верно");
            else if (_newUser.Email.Length > 40)
                errors.AppendLine("Размер почты должен быть не больше 40");
           
            if (string.IsNullOrWhiteSpace(_newUser.Logins.Password))
                errors.AppendLine("Укажите пароль!");
            else  if (_newUser.Logins.Password.Length > 30)
                errors.AppendLine("Размер пороля должен быть не больше 30");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_newUser.User_id == 0)
                BaseModel.Users.Create(_newUser);
            try
            {
                BaseModel.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
