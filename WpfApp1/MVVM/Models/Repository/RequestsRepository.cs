using Azure.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Repository
{
    internal class RequestsRepository : IRepository<Requests>
    {

        private static SUBSCRIPTION_BDEntities1 _context;
        public RequestsRepository(SUBSCRIPTION_BDEntities1 bd)
        {
            _context = bd;
        }
        public IEnumerable<Requests> GetAll()
        {
            return _context.Requests;
        }

        public Requests Get(int id)
        {
            return _context.Requests.Find(id);
        }

        public void Create(Requests type)
        {
            _context.Requests.Add(type);
        }

        public void Update(Requests type)
        {
            _context.Entry(type).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Requests type = _context.Requests.Find(id);
            if (type != null)
                _context.Requests.Remove(type);
        }
    }
}
