using Azure.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ViewModel.Subscription;

namespace WpfApp1.Models
{
    internal class SubscriptionService
    {
        public static void AcceptRequest(Requests request)
        {
            if (GetSubscriptionByType(request.User_id, request.Type_id) != null)
                return;
            Subscriptions subscriptions = new Subscriptions();
            subscriptions.Type_id = request.Type_id;
            subscriptions.User_id = request.User_id;
            BaseModel.Subscriptions.Create(subscriptions);
            BaseModel.Request.Delete(request.id);
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
        }
        public static Subscriptions GetMatchingSubscription(int userId, int pageId)
        {
            // Получаем тип подписки, связанный со страницей
            var subscriptionType = BaseModel.Context.AuthorPages
                .Where(ap => ap.AuthorPage_id == pageId)
                .SelectMany(ap => ap.Subscription_type)
                .Select(st => st.id)
                .Distinct()
                .ToList();

            if (subscriptionType == null)
            {
                return null;
            }

            Subscriptions subscription = BaseModel.Context.Subscriptions
                .Where(s => s.User_id == userId && subscriptionType.Contains(s.Type_id))
                .OrderByDescending(s => s.Subscription_type.Level)  
                .FirstOrDefault();

            return subscription;
        }
        public static Requests GetRequestByType(int userId, int typeId)
        {
            var request = BaseModel.Context.Requests
                  .FirstOrDefault(r => r.User_id == userId && r.Type_id == typeId);
            return request;
        }
        public static Subscriptions GetSubscriptionByType(int userId, int typeId)
        {
            var request = BaseModel.Context.Subscriptions
                  .FirstOrDefault(r => r.User_id == userId && r.Type_id == typeId);
            return request;
        }
        public static List<Subscriptions> GetSubscriptionsByPage(int pageId)
        {
            var subscriptionTypeIds = BaseModel.Context.AuthorPages
                .Where(ap => ap.AuthorPage_id == pageId)
                .SelectMany(ap => ap.Subscription_type)
                .Select(st => st.id)
                .Distinct()
                .ToList();

            if (!subscriptionTypeIds.Any())
            {
                return new List<Subscriptions>();
            }

            // Получаем все подписки, которые соответствуют одному из ID типов подписок
            var subscriptions = BaseModel.Context.Subscriptions
                .Where(s => subscriptionTypeIds.Contains(s.Type_id))
                .ToList();

            return subscriptions;
        }

        public static List<Requests> GetRequestsByPage(int pageId)
        {
            var subscriptionTypeIds = BaseModel.Context.AuthorPages
                .Where(ap => ap.AuthorPage_id == pageId)
                .SelectMany(ap => ap.Subscription_type)
                .Select(st => st.id)
                .Distinct()
                .ToList();

            if (!subscriptionTypeIds.Any())
            {
                return new List<Requests>();
            }

            var subscriptions = BaseModel.Context.Requests
                .Where(s => subscriptionTypeIds.Contains(s.Type_id))
                .ToList();

            return subscriptions;
        }

        public static StringBuilder CreateRequst(int userId, int typeId)
        {
            StringBuilder error = new StringBuilder();
            if (GetRequestByType(userId, typeId) != null)
                return error.Append("Запрос уже был отправлен!");
            Requests requests = new Requests();
            requests.User_id = userId;
            requests.Type_id = typeId;
            BaseModel.Request.Create(requests);
            try
            {
                BaseModel.Save();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    error.AppendLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                return error;
            }
            catch (Exception ex)
            {
                error.AppendLine(ex.ToString());
                return error;
            }
            return error;
        }
    }
}
