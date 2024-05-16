using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models.Navigation;
using WpfApp1.Models;
using WpfApp1.Pages.SettingsPages;
using WpfApp1.Pages;
using System.Windows.Navigation;

namespace WpfApp1.ViewModel
{
    internal class PageListVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AuthorPages> _authorPages;

        public ICommand CreateNewPageCommand { get; private set; }
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
        public PageListVM()
        {
            var currentList = Store.User.AuthorPages.ToList();
            AuthorPages = new ObservableCollection<AuthorPages>(currentList);

            CreateNewPageCommand = new RelayCommand(CreateNewPage);
        }
        
        private void CreateNewPage(object obj)
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPageSetting(null));
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
