using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Forms;
using WpfApp1.Models;
using WpfApp1.Pages;
using WpfApp1.Models.DataService;
using WpfApp1.Models.Navigation;
using System.Windows.Controls;

namespace WpfApp1.ViewModel
{
    internal class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
   
        private int _chosenStyle;
        private int _chosenLang;
        private ICommand _signInCommand;
        private ICommand _styleChangeCommand;
        private ICommand _langChangeCommand;
        private ICommand _navigateToUserPageCommand;
        private ICommand _goBackCommand;
        private ICommand _goForwardCommand;
        private ICommand _goHomeCommand;
        private ICommand _adminPanelCommand;



        private readonly UserDataService _dataService;
        private Users _user => _dataService.CurrentUser;

        private Frame _frame;
        public Frame MainFrame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                OnPropertyChanged(nameof(MainFrame));
            }
        }
        public Users LoginedUser
        {
            get => _user;
            set
            {
                _dataService.LoadUser(value);
                OnPropertyChanged(nameof(LoginedUser));
                OnPropertyChanged(nameof(IsGuest));
                OnPropertyChanged(nameof(IsUser));
                OnPropertyChanged(nameof(IsAdmin));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(UserAvatar));

            }
        }
        public string Name
        {
            get { return _user.Name; }
        }
       
        public string UserAvatar
        {
            get { return _user.UserAvatar; }
        }
        public bool IsGuest => LoginedUser == null;
        public bool IsUser => LoginedUser != null;
        public bool IsAdmin => LoginedUser != null && LoginedUser.Role == "admin";


        private bool _isCanGoBack;
        public bool IsCanGoBack
        {
            get { return _isCanGoBack; }
            set
            {
                _isCanGoBack = value;
                OnPropertyChanged(nameof(IsCanGoBack));
            }
        }

        private bool _isCanGoForward;
        public bool IsCanGoForward
        {
            get { return _isCanGoForward; }
            set
            {
                _isCanGoForward = value;
                OnPropertyChanged(nameof(IsCanGoForward));
            }
        }

        public ObservableCollection<string> Styles { get; set; }
        public ObservableCollection<string> Languages { get; set; }

        public ICommand NavigateToUserPageCommand => _navigateToUserPageCommand ?? (_navigateToUserPageCommand = new RelayCommand(param => NavigateToUserPage()));
        public ICommand SignInCommand => _signInCommand ?? (_signInCommand = new RelayCommand(param => SignIn()));
        public ICommand StyleChangeCommand => _styleChangeCommand ?? (_styleChangeCommand = new RelayCommand(param => ChangeStyle()));
        public ICommand LangChangeCommand => _langChangeCommand ?? (_langChangeCommand = new RelayCommand(param => ChangeLanguage()));
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(param => GoBack()));
        public ICommand GoForwardCommand => _goForwardCommand ?? (_goForwardCommand = new RelayCommand(param => GoForward()));
        public ICommand GoHomeCommand => _goHomeCommand ?? (_goHomeCommand = new RelayCommand(param => GoHome()));
        public ICommand AdminPanelCommand => _adminPanelCommand ?? (_goHomeCommand = new RelayCommand(param => AdminPanel()));

        public MainModel()
        {
            _dataService = Store.UserDataService;
            _dataService.PropertyChanged += DataService_PropertyChanged;
            Styles = new ObservableCollection<string> { "StylePurple", "StyleRed", "StyleGreen" };
            Languages = new ObservableCollection<string> { "Strings.ru-RU", "Strings.en-US" };
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new CatalogPage());
            IsCanGoBack = App.NavigationService.CanGoBack(FrameNames.MainFrame);
            IsCanGoForward = App.NavigationService.CanGoForward(FrameNames.MainFrame);
            App.NavigationService.NavigationStateChanged += NavigationService_NavigationStateChanged;
        }
        private void DataService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentUser")
            {
                OnPropertyChanged(nameof(LoginedUser));
                OnPropertyChanged(nameof(IsGuest));
                OnPropertyChanged(nameof(IsUser));
                OnPropertyChanged(nameof(IsAdmin));

            }
            OnPropertyChanged(nameof(UserAvatar));
            OnPropertyChanged(nameof(Name));
        }
        private void NavigationService_NavigationStateChanged(object sender, EventArgs e)
        {
            IsCanGoBack = App.NavigationService.CanGoBack(FrameNames.MainFrame);
            IsCanGoForward = App.NavigationService.CanGoForward(FrameNames.MainFrame);
        }
        private void GoBack()
        {
            App.NavigationService.GoBack(FrameNames.MainFrame);
        }
        private void GoForward()
        {
            App.NavigationService.GoForward(FrameNames.MainFrame);
        }
        private void GoHome()
        {
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new CatalogPage());
        }
        private void AdminPanel()
        {
            new AdminWindow().ShowDialog();
        }
        private void SignIn()
        {
            try
            {
                App.NavigationService.NavigateTo(FrameNames.MainFrame, new LoginPage());
                LoginedUser = Store.UserDataService.CurrentUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ChangeStyle()
        {
            _chosenStyle = (_chosenStyle + 1) % Styles.Count;
            UpdateResource("Style/" + Styles[_chosenStyle] + ".xaml");
        }
        private void NavigateToUserPage()
        {
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new UserPage());
        }
        private void ChangeLanguage()
        {
            _chosenLang = (_chosenLang + 1) % Languages.Count;
            UpdateResource("Languages/" + Languages[_chosenLang] + ".xaml");
        }

        private void UpdateResource(string path)
        {
            var newResource = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/WpfApp1;component/Resources/" + path)
            };
            foreach (DictionaryEntry entry in newResource)
            {
                string key = entry.Key.ToString();
                if (Application.Current.Resources.Contains(key))
                {
                    Application.Current.Resources[key] = entry.Value;
                }
                else
                {
                    Application.Current.Resources.Add(key, entry.Value);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
