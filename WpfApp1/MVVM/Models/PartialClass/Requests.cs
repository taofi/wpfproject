using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.ViewModel;
using System.Data.Entity.Validation;

namespace WpfApp1
{
    public partial class Requests
    {
        public event EventHandler RequestRejected;
        public event EventHandler RequesAccepted;

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
        private ICommand _acceptCommand;
        private ICommand _rejectCommand;

        public ICommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new RelayCommand(param => Accept()));
        public ICommand RejectCommand => _rejectCommand ?? (_rejectCommand = new RelayCommand(param => Reject()));

        private void Accept()
        {
            SubscriptionService.AcceptRequest(this);
            RequestRejected?.Invoke(this, EventArgs.Empty);
            RequesAccepted?.Invoke(this, EventArgs.Empty);

        }


        private void Reject()
        {
            BaseModel.Request.Delete(id);
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
            RequestRejected?.Invoke(this, EventArgs.Empty);
        }
    }
}
