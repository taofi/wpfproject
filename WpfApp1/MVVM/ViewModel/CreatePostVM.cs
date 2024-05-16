using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models.Navigation;
using WpfApp1.Models;
using WpfApp1.Pages;
using System.Windows;
using Microsoft.Win32;
using System.Drawing;
using System.Collections.ObjectModel;
using System.IO;
using static System.Net.WebRequestMethods;
using WpfApp1.Utilities;

namespace WpfApp1.ViewModel
{
    internal class CreatePostVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Posts _post;
        private AuthorPages _page;
        public ICommand LoadPhotoCommand { get; }
        public ICommand SaveCommand { get; }
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
            }
        }
        public Posts newPost
        {
            get { return _post; }
            set
            {
                _post = value;
                OnPropertyChanged(nameof(newPost));
            }
        }
        public string Text
        {
            get { return _post.Text; }
            set
            {
                _post.Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public string Icon
        {
            get { return _post.Files.File_url; }
            set
            {
                _post.Files.File_url = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public CreatePostVM(Posts post, AuthorPages page)
        {
            if (post == null)
            {
                _post = new Posts { Files = new Files() };
                _post.Page_id = page.AuthorPage_id;
                _post.access_level = 0;
            }
            else
            {
                _post = post;
                Icon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data", "Files", post.File_id.ToString(), post.Files.File_url); 
            }
            _page = page;
            SubscriptionsTypes = new ObservableCollection<Subscription_type>(page.Subscription_type);
            LoadPhotoCommand = new RelayCommand(_ => ExecuteLoadPhoto());
            SaveCommand = new RelayCommand(_ => ExecuteSave());
        }

        private void ExecuteLoadPhoto()
        {
        
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Icon = openFileDialog.FileName;
                OnPropertyChanged(nameof(Icon));
            }
        }

        private void ExecuteSave()
        {
            

            StringBuilder errors = Validater.Validate(newPost);
            _post.Date = DateTime.Now;
            string filePath = _post.Files.File_url;
            string fileName = Path.GetFileName(Icon);
            _post.Files.File_url = fileName;
            if (SelectedSubscriptionTypes != null)
                _post.access_level = SelectedSubscriptionTypes.Level;
            if (Validater.IsValidImageFormat(_post.Files.File_url))
                _post.Files.type = "img";
            else
                _post.Files.type = "file";
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (newPost.id == 0)
                BaseModel.Posts.Create(newPost);
            try
            {
                BaseModel.Save();
                FileService.CopyFileToFolder(filePath, _post.File_id.ToString());
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPage(_page));
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
