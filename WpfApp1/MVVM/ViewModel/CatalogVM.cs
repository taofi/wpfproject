using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.DataService;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;

namespace WpfApp1.ViewModel
{
    internal class CatalogVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AuthorPages> _authorPages;

        private readonly UserDataService _dataService;
        private Users _user => _dataService.CurrentUser;

        public bool IsUser => _user != null;
        public ObservableCollection<AuthorPages> AuthorPages
        {
            get { return _authorPages; }
            set
            {
                _authorPages = value;
                OnPropertyChanged(nameof(AuthorPages));
            }
        }

        private AuthorPages _selectedAuthorPage;
        public AuthorPages SelectedAuthorPage
        {
            get { return _selectedAuthorPage; }
            set
            {
                _selectedAuthorPage = value;
                OnPropertyChanged(nameof(SelectedAuthorPage));

                if (_selectedAuthorPage != null)
                    NavigateToAuthorPage();
            }
        }

        private string _searchStr;
        public string SearchText
        {
            get { return _searchStr; }
            set
            {
                _searchStr = value;
                OnPropertyChanged(nameof(SearchText));
                FilterAuthorPages();
            }
        }
        public CatalogVM()
        {
            _dataService = Store.UserDataService;
            _dataService.PropertyChanged += DataService_PropertyChanged;
            var currentList = BaseModel.AuthorPages.GetAll();
            AuthorPages = new ObservableCollection<AuthorPages>(currentList);
        }

        private void DataService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentUser")
            {
                OnPropertyChanged(nameof(IsUser));
            }
        }
        public void FilterAuthorPages()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                AuthorPages = new ObservableCollection<AuthorPages>(BaseModel.AuthorPages.GetAll());
            }
            else
            {
                var filtered = _authorPages.Where(ap => ap.PageName != null && ap.PageName.Contains(SearchText)).ToList();
                AuthorPages = new ObservableCollection<AuthorPages>(filtered);
            }
        }
        private void NavigateToAuthorPage()
        {
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new AuthorPage(_selectedAuthorPage));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
