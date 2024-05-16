using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;

namespace WpfApp1.ViewModel.Subscription
{
    internal class SubscriptionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AuthorPages _page;
        private ObservableCollection<Subscription_type> _subscriptionsType;


        public ObservableCollection<Subscription_type> SubscriptionsTypes
        {
            get { return _subscriptionsType; }
            set
            {
                _subscriptionsType = value;
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
            }
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
        public string PageIcon
        {
            get { return _page.PageIcon; }
            set
            {
                _page.PageIcon = value;
                OnPropertyChanged(PageIcon);
            }
        }
        public ICommand SendRequestCommand { get; }
        public ICommand BackCommand { get; }

        public SubscriptionVM(AuthorPages page)
        {
            Page = page;
            SubscriptionsTypes = new ObservableCollection<Subscription_type>( page.Subscription_type); 
            BackCommand = new RelayCommand(_ => Back());
            SendRequestCommand = new RelayCommand(_ => SendRequest());
        }

        public void Back()
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPage(Page));
        }
        public void SendRequest()
        {
            if (SelectedSubscriptionTypes == null)
            {
                MessageBox.Show("Подписка не выбрана");
                return;
            }
            StringBuilder error = SubscriptionService.CreateRequst(Store.User.User_id, SelectedSubscriptionTypes.id);
            if(error.Length > 0)
            {
                MessageBox.Show(error.ToString());
            }
            else
                MessageBox.Show("Запрос отправлен");
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPage(Page));
        }
        protected void OnPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
