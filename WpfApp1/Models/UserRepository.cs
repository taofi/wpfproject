using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class UserRepository : IRepository<Users>
    {

        private static SUBSCRIPTION_BDEntities _context;
        public UserRepository(SUBSCRIPTION_BDEntities bd)
        {
            _context = bd;
        }
        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }

        public Users Get(int id)
        {
            return _context.Users.Find(id);
        }

        public void Create(Users user)
        {
            _context.Users.Add(user);
        }

        public void Update(Users user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Users user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }
    }
}
