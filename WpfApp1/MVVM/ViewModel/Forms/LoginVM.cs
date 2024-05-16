using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp1.Forms;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.Utilities;

namespace WpfApp1.ViewModel.Forms
{
    internal class LoginVM
    {

        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
            }
        }

        public ICommand SignInCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginVM()
        {
            SignInCommand = new RelayCommand(param => SignIn());
            RegisterCommand = new RelayCommand(param => Register());
        }

        private void SignIn()
        {
            string HashPassword = Validater.HashPassword(Password);
            if (Store.Login(new Logins { Login = this.Login, Password = HashPassword }))
            {
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CatalogPage());
            }
            else
            {
                MessageBox.Show("Не верный логин или пароль");
            }
        }

        private void Register()
        {
           App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new RegisterPage());
        }
    }
}
