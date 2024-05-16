using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModel
{
    internal class AdminVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Users> _users;

        public ObservableCollection<Users> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public AdminVM()
        {
            _users = new ObservableCollection<Users>();
            Load();
        }
        private void Load()
        {
            foreach (var request in BaseModel.Users.GetAll())
            {
                request.UserDeleted += User_Deleted;
                Users.Add(request);
            }
        }
        private void User_Deleted(object sender, EventArgs e)
        {
            if (sender is Users request)
            {
                App.Current.Dispatcher.Invoke(() => Users.Remove(request));
            }
        }
        protected void OnPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
