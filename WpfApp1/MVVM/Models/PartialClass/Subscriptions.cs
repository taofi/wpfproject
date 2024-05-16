using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class Subscriptions
    {
        public event EventHandler SubscriptionsDeleted;

        public string UserName
        {
            get => Users.Name;
        }
        public string UserLogin
        {
            get => Users.Login;
        }
        public string SubscriptionsName
        {
            get => Subscription_type.Name;
        }
        public string Level
        {
            get => Subscription_type.Level.ToString();
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(param => Delete()));

        private void Delete()
        {
            BaseModel.Subscriptions.Delete(id);
            try
            {
                BaseModel.Save();
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
            SubscriptionsDeleted?.Invoke(this, EventArgs.Empty);
        }

    }
}
