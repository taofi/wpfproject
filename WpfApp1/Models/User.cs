using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public User(Users user)
        {
            User_id = user.User_id;
            Name = user.Name;
            Login = user.Login;
            Password = user.Logins.Password;
            Email = user.Email;
            Role = user.Role;
            UserAvatar = user.UserAvatar;
        }
        public int User_id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Login { get; set; }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        public string Role { get; set; }
        private string _userAvatar;
        public string UserAvatar
        {
            get { return _userAvatar; }
            set { _userAvatar = value; OnPropertyChanged(nameof(UserAvatar)); }
        }

    }
}
