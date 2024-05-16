using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApp1.Models.Repository;
using WpfApp1.Utilities;


namespace WpfApp1.Models
{
    internal class BaseModel
    {
        private static SUBSCRIPTION_BDEntities1 _context;

        private static UserRepository userRepository;
        private static AuthorPagesRepository authorRepository;
        private static PostsRepository postsRepository;
        private static SubscriptionTypeRepository subscriptionTypeRepository;
        private static SubscriptionRepository subscriptionRepository;
        private static RequestsRepository requestsRepository;
        private static CommentsRepository commentsRepository;




        public static SUBSCRIPTION_BDEntities1 Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new SUBSCRIPTION_BDEntities1();
                    if(!_context.Logins.Any(l => l.Login== "admin"))
                    {
                        Users admin = new Users { Logins = new Logins() };
                        admin.Role = "admin";
                        admin.Name = "admin";
                        admin.Logins.Login = "admin";
                        admin.Logins.Password = Validater.HashPassword("123");
                        admin.Login = "admin";
                        Users.Create(admin);
                        Save();
                    }
                }

                return _context;
            }
        }

        public static UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(Context);
                return userRepository;
            }
        }

        public static AuthorPagesRepository AuthorPages
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorPagesRepository(Context);
                return authorRepository;
            }
        }

        public static PostsRepository Posts
        {
            get
            {
                if (postsRepository == null)
                    postsRepository = new PostsRepository(Context);
                return postsRepository;
            }
        }

        public static SubscriptionTypeRepository SubscriptionType
        {
            get
            {
                if (subscriptionTypeRepository == null)
                    subscriptionTypeRepository = new SubscriptionTypeRepository(Context);
                return subscriptionTypeRepository;
            }
        }
        public static SubscriptionRepository Subscriptions
        {
            get
            {
                if (subscriptionRepository == null)
                    subscriptionRepository = new SubscriptionRepository(Context);
                return subscriptionRepository;
            }
        }
        public static RequestsRepository Request
        {
            get
            {
                if (requestsRepository == null)
                    requestsRepository = new RequestsRepository(Context);
                return requestsRepository;
            }
        }
        public static CommentsRepository Comments
        {
            get
            {
                if (commentsRepository == null)
                    commentsRepository = new CommentsRepository(Context);
                return commentsRepository;
            }
        }
        public static void Save()
        {
            Context.SaveChanges();
        }


    }
}
