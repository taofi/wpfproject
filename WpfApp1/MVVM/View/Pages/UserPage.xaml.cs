using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.IdentityModel.Protocols.WSTrust;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Validation;
using WpfApp1.Pages.SettingsPages;
using WpfApp1.ViewModel;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            DataContext = new UserPageVM();     
        }


    }
}
