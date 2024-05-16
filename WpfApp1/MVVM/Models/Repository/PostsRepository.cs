using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Repository
{
    internal class PostsRepository : IRepository<Posts>
    {

        private static SUBSCRIPTION_BDEntities1 _context;
        public PostsRepository(SUBSCRIPTION_BDEntities1 bd)
        {
            _context = bd;
        }
        public IEnumerable<Posts> GetAll()
        {
            return _context.Posts;
        }

        public Posts Get(int id)
        {
            return _context.Posts.Find(id);
        }

        public void Create(Posts Post)
        {
            _context.Posts.Add(Post);
        }

        public void Update(Posts Post)
        {
            _context.Entry(Post).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Posts Post = _context.Posts.Find(id);
            if (Post != null)
            {
                FileService.DeleteFolder(Post.File_id.ToString());
                foreach (Comments comment in Post.Comments.ToList())
                    BaseModel.Comments.Delete(comment.Comment_id);
                _context.Files.Remove(Post.Files);
                _context.Posts.Remove(Post);
            }
        }
    }
}
