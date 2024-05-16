using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;

namespace WpfApp1.ViewModel
{
    internal class RequestsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Requests> _requests;
        private ObservableCollection<Subscriptions> _subscriptions;

        private AuthorPages _page;
        public AuthorPages Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }
        public string PageIcon
        {
            get => _page.PageIcon;
            set
            {
                _page.PageIcon = value;
                OnPropertyChanged(nameof(PageIcon));
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
        public ObservableCollection<Subscriptions> Subscriptions
        {
            get { return _subscriptions; }
            set
            {
                _subscriptions = value;
                OnPropertyChanged(nameof(_subscriptions));
            }
        }
        private Subscriptions _selectedSubscriptions;
        public Subscriptions SelectedSubscriptions
        {
            get { return _selectedSubscriptions; }
            set
            {
                _selectedSubscriptions = value;
                OnPropertyChanged(nameof(SelectedSubscriptions));
            }
        }
        public ObservableCollection<Requests> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        private Requests _selectedRequests;
        public Requests SelectedRequests
        {
            get { return _selectedRequests; }
            set
            {
                _selectedRequests = value;
                OnPropertyChanged(nameof(SelectedRequests));
            }
        }
        public ICommand BackCommand { get; }

        public RequestsVM(AuthorPages page)
        {
            _page = page;
            Requests = new ObservableCollection<Requests>();
            Subscriptions = new ObservableCollection<Subscriptions>();

            BackCommand = new RelayCommand(_ => Back());
            LoadSubscriptions();
            LoadRequests();
        }
        private void LoadRequests()
        {
            var loadedRequests = SubscriptionService.GetRequestsByPage(Page.AuthorPage_id);
            foreach (var request in loadedRequests)
            {
                request.RequestRejected += Request_RequestRejected;
                request.RequesAccepted += Request_RequestAccepted;

                Requests.Add(request);
            }
        }
        private void LoadSubscriptions()
        {
            var loadedRequests = SubscriptionService.GetSubscriptionsByPage(Page.AuthorPage_id);
            foreach (var request in loadedRequests)
            {
                request.SubscriptionsDeleted += Subscriptions_Deleted;
                Subscriptions.Add(request);
            }
        }
        public void Back()
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPage(Page));
        }
        private void Subscriptions_Deleted(object sender, EventArgs e)
        {
            if (sender is Subscriptions request)
            {
                App.Current.Dispatcher.Invoke(() => Subscriptions.Remove(request)); 
            }
        }
        private void Request_RequestRejected(object sender, EventArgs e)
        {
            if (sender is Requests request)
            {
                App.Current.Dispatcher.Invoke(() => Requests.Remove(request));
            }
        }
        private void Request_RequestAccepted(object sender, EventArgs e)
        {
            if (sender is Requests request)
            {
                App.Current.Dispatcher.Invoke(() => Requests.Remove(request));
                Subscriptions subscriptions = SubscriptionService.GetSubscriptionByType(request.User_id, request.Type_id);
                App.Current.Dispatcher.Invoke(() => Subscriptions.Add(subscriptions));
                subscriptions.SubscriptionsDeleted += Subscriptions_Deleted;
            }
        }
        protected void OnPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
