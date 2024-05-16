using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models.Navigation;
using WpfApp1.Models;
using WpfApp1.Pages.SettingsPages;
using WpfApp1.Pages;
using System.Windows;
using Microsoft.Win32;
using WpfApp1.Forms;
using System.Collections.ObjectModel;
using WpfApp1.View.Pages;

namespace WpfApp1.ViewModel
{
    internal class AuthorPageSettingVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AuthorPages _page;

        private ObservableCollection<Subscription_type> _subscriptions;

        public ObservableCollection<Subscription_type> SubscriptionsTypes
        {
            get { return _subscriptions; }
            set
            {
                _subscriptions = value;
                OnPropertyChanged(nameof(SubscriptionsTypes));
            }
        }
        private Subscription_type _selectedSubscriptionsTypes;
        public Subscription_type SelectedSubscriptionTypes
        {
            get { return _selectedSubscriptionsTypes; }
            set
            {
                _selectedSubscriptionsTypes = value;
                OnPropertyChanged(nameof(_selectedSubscriptionsTypes));

                if (_selectedSubscriptionsTypes != null)
                    NavigateToSubscriptions();
            }
        }
        private void NavigateToSubscriptions()
        {
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new CreateSubscriptionTypesPage(SelectedSubscriptionTypes, _page));
        }
        public AuthorPages Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }
       
        public string PageName
        {
            get { return _page.PageName; }
            set
            {
                _page.PageName = value;
                OnPropertyChanged(PageName);
            }
        }
        public string Titel_text
        {
            get { return _page.Titel_text; }
            set
            {
                _page.Titel_text = value;
                OnPropertyChanged(Titel_text);
            }
        }

        public string PageIcon
        {
            get { return _page.PageIcon; }
            set
            {
                _page.PageIcon = value;
                OnPropertyChanged(PageIcon);
            }
        }

        public ICommand LoadPhotoCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }


        public AuthorPageSettingVM(AuthorPages page)
        {
            if (page == null)
            {
                _page = new AuthorPages();
            }
            else
                _page = page;
            _page.Owner = Store.User.User_id;

            SubscriptionsTypes = new ObservableCollection<Subscription_type>(_page.Subscription_type);
            LoadPhotoCommand = new RelayCommand(_ => ExecuteLoadPhoto());
            SaveCommand = new RelayCommand(_ => ExecuteSave());
            AddCommand = new RelayCommand(_ => Add());
        }
        private void Add()
        {
            if (_page.AuthorPage_id == 0)
                MessageBox.Show("Сохраните страницу!");
            else
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CreateSubscriptionTypesPage(null, _page));
        }
        private void ExecuteLoadPhoto()
        {
            if (Store.User != null && Store.User.User_id != _page.Owner)
            {
                MessageBox.Show("Вы не владелец страницы");
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CatalogPage());
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.jfif)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.jfif|All files (*.*)|*.*"

            };
            if (openFileDialog.ShowDialog() == true)
            {
                PageIcon = openFileDialog.FileName;
                OnPropertyChanged(nameof(PageIcon));
            }
        }


        private void ExecuteSave()
        {
            StringBuilder errors = new StringBuilder();
           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if(Page.AuthorPage_id == 0)
                BaseModel.AuthorPages.Create(Page);
            try
            {
                BaseModel.Save();
                MessageBox.Show("saved");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
