using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp1.Forms;
using WpfApp1.Models;
using WpfApp1.Models.DataService;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.Pages.SettingsPages;
using WpfApp1.View.Pages;
using WpfApp1.View.Pages.Subscription;

namespace WpfApp1.ViewModel
{
    internal class AuthorPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Posts> _posts;
        public ObservableCollection<Posts> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }


        private readonly UserDataService _dataService;
        public Users User
        {
            get => _dataService.CurrentUser;
        }
        private AuthorPages _currentPage;
        public AuthorPages CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public string PageIcon
        {
            get => _currentPage.PageIcon;
            set
            {
                _currentPage.PageIcon = value;
                OnPropertyChanged(nameof(PageIcon));
            }
        }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand SubscribeCommand { get; private set; }
        public ICommand CreatePostCommand { get; private set; }
        public ICommand SeeRequestsCommand { get; private set; }


        public AuthorPageVM(AuthorPages currentPage)
        {
            _dataService = Store.UserDataService;
            _dataService.PropertyChanged += DataService_PropertyChanged;
            CurrentPage = currentPage;
            DeleteCommand = new RelayCommand(_ => DeleteAction());
            EditCommand = new RelayCommand(_ =>EditAction());
            SubscribeCommand = new RelayCommand(_ => SubscribeAction());
            CreatePostCommand = new RelayCommand(_ => CreatePost());
            SeeRequestsCommand = new RelayCommand(_ => SeeRequests());
            Posts = new ObservableCollection<Posts>(CurrentPage.Posts.OrderByDescending(post => post.Date).ToList());
        }
        private void DataService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentUser")
            {
                OnPropertyChanged(nameof(IsOwner));
                OnPropertyChanged(nameof(IsNotOwner));

            }
        }

        public bool IsOwner => (User != null && User.User_id == CurrentPage.Owner)
                                || (Store.User != null && Store.User.Role == "admin");
        public bool IsNotOwner => !IsOwner;
        

        private void DeleteAction()
        {
            BaseModel.AuthorPages.Delete(_currentPage.AuthorPage_id);
            try
            {
                BaseModel.Save();
                MessageBox.Show("Страница удалена!");
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder error = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    error.AppendLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                MessageBox.Show(error.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CatalogPage());
            App.NavigationService.ClearNavigationStack(FrameNames.MainFrame);
            OnPropertyChanged(nameof(PageIcon));
            OnPropertyChanged(nameof(IsOwner));
            OnPropertyChanged(nameof(IsNotOwner));
        }

        private void EditAction()
        {
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new AuthorPageSetting(CurrentPage));
        }
        private void SeeRequests()
        {
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new RequestsPage(CurrentPage));
        }

        private void SubscribeAction()
        {
            if (Store.User != null)
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new SubscriptionPage(CurrentPage));
            else
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new LoginPage());
        }

        


        private void CreatePost()
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CreatePostPage(null, CurrentPage));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
