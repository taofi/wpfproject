using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class AuthorPagesRepository : IRepository<AuthorPages>
    {

        private static SUBSCRIPTION_BDEntities1 _context;
        public AuthorPagesRepository(SUBSCRIPTION_BDEntities1 bd)
        {
            _context = bd;
        }
        public IEnumerable<AuthorPages> GetAll()
        {
            return _context.AuthorPages;
        }

        public AuthorPages Get(int id)
        {
            return _context.AuthorPages.Find(id);
        }

        public void Create(AuthorPages Page)
        {
            _context.AuthorPages.Add(Page);
        }

        public void Update(AuthorPages Page)
        {
            _context.Entry(Page).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            AuthorPages Page = _context.AuthorPages.Find(id);
            if (Page != null)
            {
                foreach (Posts post in Page.Posts.ToList())
                    BaseModel.Posts.Delete(post.id);
                foreach (Subscription_type subscriptionType in Page.Subscription_type.ToList())
                    BaseModel.SubscriptionType.Delete(subscriptionType.id);
                _context.AuthorPages.Remove(Page);
            }
        }
    }
}
