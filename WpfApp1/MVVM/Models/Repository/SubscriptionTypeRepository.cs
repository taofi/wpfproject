using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Repository
{
    internal class SubscriptionTypeRepository : IRepository<Subscription_type>
    {

        private static SUBSCRIPTION_BDEntities1 _context;
        public SubscriptionTypeRepository(SUBSCRIPTION_BDEntities1 bd)
        {
            _context = bd;
        }
        public IEnumerable<Subscription_type> GetAll()
        {
            return _context.Subscription_type;
        }

        public Subscription_type Get(int id)
        {
            return _context.Subscription_type.Find(id);
        }

        public void Create(Subscription_type type)
        {
            _context.Subscription_type.Add(type);
        }

        public void Update(Subscription_type type)
        {
            _context.Entry(type).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Subscription_type type = _context.Subscription_type.Find(id);
            if (type != null)
            {
                foreach (Subscriptions subscription in type.Subscriptions.ToList())
                    BaseModel.Subscriptions.Delete(subscription.id);
                foreach (Requests requests in type.Requests.ToList())
                    BaseModel.Request.Delete(requests.id);
                _context.Subscription_type.Remove(type);
            }
        }
    }
}
