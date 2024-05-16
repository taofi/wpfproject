using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.View.Pages;

namespace WpfApp1
{
    public partial class Posts
    {
        public bool IsImg => Files != null && Files.type == "img";
        public bool IsNotImg => !IsImg;
        public string url => Files.File_url;
        private bool Visibility
        {
            get
            {
                if (access_level == 0 || Store.User != null && AuthorPages.Owner == Store.User.User_id)
                    return true;
                if (access_level != 0 && Store.User == null)
                    return false;

                var subscription = SubscriptionService.GetMatchingSubscription(Store.User.User_id, AuthorPages.AuthorPage_id);
                return Store.User != null && subscription != null && subscription.Subscription_type.Level >= access_level;
            }
        }
        public Files file
        {
            get
            {
                return Files;
            }
        }
        public bool IsVisible => Visibility;
        public bool IsNotVisible => !Visibility;


        private ICommand _deleteCommand;
        private ICommand _editCommand;
        private ICommand _commentCommand;
        private ICommand _downloadCommand;


        public ICommand CommentCommand => _commentCommand ?? (_commentCommand = new RelayCommand(param => Comment()));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(param => DeletePost()));
        public ICommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(param => EditPost()));
        public ICommand DownloadCommand => _downloadCommand ?? (_downloadCommand = new RelayCommand(param => Download()));

        private void Comment()
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CommentsPage(this));
        }

        private void Download()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Выберите папку"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string folderPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                FileService.DownloadFileToFolder(file.File_id.ToString(), file.File_url ,folderPath);
            }
        }
        private void DeletePost()
        {
            BaseModel.Posts.Delete(id);
            try
            {
                BaseModel.Save();
                App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPage(BaseModel.AuthorPages.Get(Page_id)));
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

        private void EditPost()
        {
            Posts editPost = BaseModel.Posts.Get(id);
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new CreatePostPage(editPost, BaseModel.AuthorPages.Get(Page_id)));

        }
    }
}
