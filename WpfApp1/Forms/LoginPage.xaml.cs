using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public delegate void Delegate();
        public event Delegate UserLoggedIn;
        private Logins _login = new Logins();
        public LoginPage()
        {
            InitializeComponent();
            DataContext = _login;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage());
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (Store.Login(_login))
                MessageBox.Show("Вы успешно вошли");
            else
                MessageBox.Show("Не верный логин или пароль");

        }

    }
}
