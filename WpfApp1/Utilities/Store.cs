using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Models.DataService;
using WpfApp1.Utilities;

namespace WpfApp1.Models
{
    internal class Store
    {
        private static UserDataService _currentUser;
        static Store()
        {
            _currentUser = new UserDataService();
        }
        public static UserDataService UserDataService
        {
            get
            {
                return _currentUser;
            }
        }
        public static Users User
        {
            get
            {
                return _currentUser.CurrentUser;
            }
        }
       

        public static bool Login(Logins _login)
        {
            var user = BaseModel.Context.Logins.FirstOrDefault(l => l.Login == _login.Login && l.Password == _login.Password);

            if (user != null)
            {
                _currentUser.LoadUser(FindUserByLogin(_login));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SignOut()
        {
            _currentUser.LoadUser(null);
        }
        private static Users FindUserByLogin(Logins login)
        {
            var user = BaseModel.Users.GetAll().FirstOrDefault(u => u.Login == login.Login);

            if (user != null)
            {
                user.Logins.Password = login.Password;
                return user;
            }
            else
                return null;
        }
    }
}
