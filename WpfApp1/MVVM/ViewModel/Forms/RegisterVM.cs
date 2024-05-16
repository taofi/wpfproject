using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Forms;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.Utilities;

namespace WpfApp1.ViewModel.Forms
{
    internal class RegisterVM
    {
        public Users NewUser { get; set; }

        public ICommand RegisterCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }

        public RegisterVM()
        {
            NewUser = new Users { Logins = new Logins() };
            RegisterCommand = new RelayCommand(_ => RegisterUser());
            GoBackCommand = new RelayCommand(_ => GoBack());
        }

        private void RegisterUser()
        {
            NewUser.Role = "user";
            StringBuilder errors = Validater.Validate(NewUser);

            if (string.IsNullOrWhiteSpace(NewUser.Logins.Password))
                errors.AppendLine("Укажите пароль!");
            else if (NewUser.Logins.Password.Length > 30)
                errors.AppendLine("Размер пороля должен быть не больше 30");
            if (BaseModel.Context.Logins.Any((login => login.Login == NewUser.Logins.Login)))
            {
                errors.Append("Логин должен быть уникальным");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            NewUser.Logins.Password = Validater.HashPassword(NewUser.Logins.Password);
            if (NewUser.User_id == 0)
                BaseModel.Users.Create(NewUser);

            try
            {
                BaseModel.Save();
                MessageBox.Show("успешно");
                GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        

        private void GoBack()
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new LoginPage());
        }
    }
}
