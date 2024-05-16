using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.DataService;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.Pages.SettingsPages;
using WpfApp1.Utilities;

namespace WpfApp1.ViewModel
{
    internal class UserPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Users CurrentUser;
        private CustomNavigationService _navigationService;

        private Frame _userPageFrame;
        public Frame UserPageFrame
        {
            get { return _userPageFrame; }
            set
            {
                _userPageFrame = value;
                OnPropertyChanged(nameof(UserPageFrame));
            }
        }
        public Users User
        {
            get => CurrentUser;
            set
            {
                CurrentUser = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public string Name
        {
            get { return CurrentUser.Name; }
            set
            {
                CurrentUser.Name = value;
                OnPropertyChanged(Name);
            }
        }
        public string Email
        {
            get { return CurrentUser.Email; }
            set
            {
                CurrentUser.Email = value;
                OnPropertyChanged(Email);
            }
        }

        public string UserAvatar
        {
            get { return CurrentUser.UserAvatar; }
            set
            {
                CurrentUser.UserAvatar = value;
                OnPropertyChanged(UserAvatar);
            }
        }

        public ICommand LoadPhotoCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExitCommand { get; }


        public UserPageVM()
        {
            _navigationService = new CustomNavigationService();
            UserPageFrame = new Frame();
            _navigationService.RegisterFrame(FrameNames.UserPageFrame, UserPageFrame);
            CurrentUser = Store.User;
            if (!CurrentUser.AuthorPages.Any())
                _navigationService.NavigateTo(FrameNames.UserPageFrame, new CreateAuthorPage());
            else
                _navigationService.NavigateTo(FrameNames.UserPageFrame, new PageList());

            ExitCommand = new RelayCommand(_ => ExitUser());
            LoadPhotoCommand = new RelayCommand(_ => ExecuteLoadPhoto());
            SaveCommand = new RelayCommand(_ => ExecuteSave());
        }
     
        private void ExecuteLoadPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.jfif)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.jfif|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                UserAvatar = openFileDialog.FileName;
                OnPropertyChanged(nameof(UserAvatar));
            }
        }

        private void ExitUser()
        {
            Store.SignOut();
            App.NavigationService.NavigateTo(FrameNames.MainFrame, new CatalogPage());
        }

        private void ExecuteSave()
        {
            StringBuilder errors = new StringBuilder();
            errors = Validater.Validate(User);
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                BaseModel.Save();
                Store.UserDataService.LoadUser(CurrentUser);
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
                MessageBox.Show(ex.ToString() + " id: " + CurrentUser.User_id);
            }
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
