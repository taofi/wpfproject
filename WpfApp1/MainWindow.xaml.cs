using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using WpfApp1.Forms;
using WpfApp1.Models;
using WpfApp1.Pages;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Users _loginedUser;
        private int _chosenStyle = 0;
        private int _chosenLang = 0;
       
        public MainWindow()
        {
            InitializeComponent();
            UpdateUserInterface();
            DataContext = _loginedUser;
            MainFrame.Navigate(new CatalogPage());
        }
        private void UserIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MainFrame.Navigate(new UserPage());
        }
        private void UserIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void UserIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                _loginedUser = Store.User;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            UpdateUserInterface();
        }
        private void UpdateUserInterface()
        {
            if (_loginedUser != null)
            {
                GuestPanel.Visibility = Visibility.Collapsed;
                UserPanel.Visibility = Visibility.Visible;
                UserNameLabel.Content = _loginedUser.Name;
                ChangeImage(_loginedUser.UserAvatar);
            }
            else
            {
                GuestPanel.Visibility = Visibility.Visible;
                UserPanel.Visibility = Visibility.Collapsed;
            }
        }


        private void ChangeImage(string src)
        {
            if (src == null)
                return;
            BitmapImage newImage = new BitmapImage();
            newImage.BeginInit();
            newImage.UriSource = new Uri(src);
            newImage.EndInit();
            UserIcon.Source = newImage;
        }

        private void StyleСhange_Сlick(object sender, RoutedEventArgs e)
        {
            List<string> _styles = new List<string> { "StylePurple", "StyleRed", "StyleGreen" };
            _chosenStyle++;
            if (_chosenStyle == _styles.Count)
                _chosenStyle = 0;
            string newStyle = "Resources/Style/" + _styles[_chosenStyle] + ".xaml";
           
            UpdateAllGlobalResources(newStyle);

        }
      


        void UpdateAllGlobalResources(string newStyle)
        {
            var sourceDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/WpfApp1;component/" + newStyle)
            };
            foreach (DictionaryEntry entry in sourceDictionary)
            {
                if (entry.Key is string resourceKey)
                {
                    if (Application.Current.Resources.Contains(resourceKey))
                    {
                        Application.Current.Resources[resourceKey] = entry.Value;
                    }
                    else
                    {
                        Application.Current.Resources.Add(resourceKey, entry.Value);
                    }
                }
            }
        }


        private void LangСhange_Сlick(object sender, RoutedEventArgs e)
        {
            List<string> _lang = new List<string> { "Strings.ru-RU", "Strings.en-US" };
            _chosenLang++;
            if (_chosenLang == _lang.Count)
                _chosenLang = 0;
            string newStyle = "Resources/Languages/" + _lang[_chosenStyle] + ".xaml";
            UpdateAllGlobalResources(newStyle);
        }
    }
}
