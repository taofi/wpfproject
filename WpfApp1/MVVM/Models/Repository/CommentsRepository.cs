using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Repository
{
    internal class CommentsRepository : IRepository<Comments>
    {

        private static SUBSCRIPTION_BDEntities1 _context;
        public CommentsRepository(SUBSCRIPTION_BDEntities1 bd)
        {
            _context = bd;
        }
        public IEnumerable<Comments> GetAll()
        {
            return _context.Comments;
        }

        public Comments Get(int id)
        {
            return _context.Comments.Find(id);
        }

        public void Create(Comments Post)
        {
            _context.Comments.Add(Post);
        }

        public void Update(Comments Post)
        {
            _context.Entry(Post).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Comments Post = _context.Comments.Find(id);
            if (Post != null)
            {
                _context.Comments.Remove(Post);
            }
        }
    }
}
