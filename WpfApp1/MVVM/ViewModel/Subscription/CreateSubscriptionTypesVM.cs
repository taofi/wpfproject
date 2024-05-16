using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.Pages.SettingsPages;
using WpfApp1.Utilities;

namespace WpfApp1.ViewModel
{
    internal class CreateSubscriptionTypesVM : INotifyPropertyChanged
    {
        private Subscription_type _subscription_Type;
        private AuthorPages _page;
        public string Name
        {
            get => _subscription_Type.Name;
            set
            {
                _subscription_Type.Name = value;
            }
        }
        public int Level
        {
            get => _subscription_Type.Level;
            set
            {
                if (value < 1)
                    value = 1;  
                else if (value > 100)
                    value = 100; 

                if (_subscription_Type.Level != value)
                {
                    _subscription_Type.Level = value;
                    OnPropertyChanged(nameof(Level));
                }
            }
        } 
        
        public ICommand SaveCommand { get; }
        public CreateSubscriptionTypesVM(Subscription_type type, AuthorPages page)
        {
            _page = page;
            if (type == null)
            {
                _subscription_Type = new Subscription_type();
                _subscription_Type.Level = 1;
            }
            else
                _subscription_Type = type;
            _subscription_Type.Page_id = page.AuthorPage_id;
            SaveCommand = new RelayCommand(param => Save());
        }
        private void Save()
        {
            StringBuilder errors = Validater.Validate(_subscription_Type);
           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_subscription_Type.id == 0)
                BaseModel.SubscriptionType.Create(_subscription_Type);

            try
            {
                BaseModel.Save();
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPageSetting(_page));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
