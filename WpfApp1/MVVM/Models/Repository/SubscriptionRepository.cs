using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Repository
{
    internal class SubscriptionRepository : IRepository<Subscriptions>
    {

        private static SUBSCRIPTION_BDEntities1 _context;
        public SubscriptionRepository(SUBSCRIPTION_BDEntities1 bd)
        {
            _context = bd;
        }
        public IEnumerable<Subscriptions> GetAll()
        {
            return _context.Subscriptions;
        }

        public Subscriptions Get(int id)
        {
            return _context.Subscriptions.Find(id);
        }

        public void Create(Subscriptions type)
        {
            _context.Subscriptions.Add(type);
        }

        public void Update(Subscriptions type)
        {
            _context.Entry(type).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Subscriptions type = _context.Subscriptions.Find(id);
            if (type != null)
                _context.Subscriptions.Remove(type);
        }
    }
}
