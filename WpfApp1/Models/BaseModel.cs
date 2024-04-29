using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class BaseModel
    {
        private static SUBSCRIPTION_BDEntities _context;

        private static UserRepository userRepository;
        //public static SUBSCRIPTION_BDEntities GetContext()
        //{
        //    if (_context == null)
        //        _context = new SUBSCRIPTION_BDEntities();
        //    return _context;
        //}

        public static SUBSCRIPTION_BDEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new SUBSCRIPTION_BDEntities();
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

        public static void Save()
        {
            Context.SaveChanges();
        }


    }
}
