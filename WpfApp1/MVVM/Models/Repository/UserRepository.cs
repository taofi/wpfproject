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

        private static SUBSCRIPTION_BDEntities1 _context;
        public UserRepository(SUBSCRIPTION_BDEntities1 bd)
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
            {
                foreach (Subscriptions subscription in user.Subscriptions.ToList())
                    BaseModel.Subscriptions.Delete(subscription.id);
                BaseModel.Save();
                foreach (Requests requests in user.Requests.ToList())
                    BaseModel.Request.Delete(requests.id);
                BaseModel.Save();
                foreach (AuthorPages page in user.AuthorPages.ToList())
                    BaseModel.AuthorPages.Delete(page.AuthorPage_id);
                BaseModel.Save();
                foreach (Comments comment in user.Comments.ToList())
                    BaseModel.Comments.Delete(comment.Comment_id);
                BaseModel.Save();
                Logins login = _context.Logins.Find(user.Login);
                if (login != null)
                    _context.Logins.Remove(login);
                _context.Users.Remove(user);


            }
        }
    }
}
