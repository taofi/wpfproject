using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.DataService
{
    internal class UserDataService : INotifyPropertyChanged
    {
        private Users _currentUser;

        public event PropertyChangedEventHandler PropertyChanged;

        public Users CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public string Name
        {
            get { return CurrentUser.Name; }
            set
            {
                CurrentUser.Name = value;
                OnPropertyChanged(Name);
            }
        }
        public string Email
        {
            get { return CurrentUser.Email; }
            set
            {
                CurrentUser.Email = value;
                OnPropertyChanged(Email);
            }
        }
        public string UserAvatar
        {
            get { return CurrentUser.UserAvatar; }
            set
            {
                CurrentUser.UserAvatar = value;
                OnPropertyChanged(UserAvatar);
            }
        }

        public void LoadUser(Users user)
        {
            CurrentUser = user;
        }

     
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

