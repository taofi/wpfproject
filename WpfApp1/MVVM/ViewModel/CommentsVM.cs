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

namespace WpfApp1.ViewModel
{
    internal class CommentsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Posts _post;
        private string _text;
        
        private ObservableCollection<Comments> _comments;

        public ObservableCollection<Comments> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public string PostText
        {
            get { return _post.Text; }
        }
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public string PostDate
        {
            get { return _post.Date.ToString(); }
        }
        public Files file
        {
            get { return _post.Files; }
        }
        public bool IsUser => Store.User != null;

        public ICommand SendCommand { get; }
        public CommentsVM(Posts posts)
        {
            _post = posts;
            Comments = new ObservableCollection<Comments>();
            SendCommand = new RelayCommand(_ => Send());
            LoadComments();
        }
        private void LoadComments()
        {
            var comments = _post.Comments;
            foreach (var comment in comments)
            {
                comment.CommentDeleted += CommentDeleted;
                Comments.Add(comment);
            }
        }
        private void CommentDeleted(object sender, EventArgs e)
        {
            if (sender is Comments comment)
            {
                App.Current.Dispatcher.Invoke(() => Comments.Remove(comment));
            }
        }
        private void Send()
        {
            Comments comments = new Comments();
            if (Text.Length <= 0)
                return;
            comments.Post_id = _post.id;
            comments.Text = Text;
            comments.User_id = Store.User.User_id;
            comments.Date = DateTime.Now;
            BaseModel.Comments.Create(comments);
            try
            {
                BaseModel.Save();
                Comments.Add(comments);
                comments.CommentDeleted += CommentDeleted;

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
