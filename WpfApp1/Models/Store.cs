using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class Store
    {
        private static Users _currentUser;

        public static Users User
        {
            get
            {
                return _currentUser;
            }
        }

        public static bool Login(Logins _login)
        {
            var user = BaseModel.Context.Logins.FirstOrDefault(l => l.Login == _login.Login && l.Password == _login.Password);

            if (user != null)
            {
                _currentUser = FindUserByLogin(_login.Login);
                return true;
            }
            else
            {
                return false;
            }
        }
        private static Users FindUserByLogin(string login)
        {
            Users user = BaseModel.Users.GetAll().FirstOrDefault(u => u.Login == login);

            if (user != null)
                return user;
            else
                return null;
        }
    }
}
