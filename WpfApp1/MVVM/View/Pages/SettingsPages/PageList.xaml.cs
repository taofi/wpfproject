using System;
using System.Collections.Generic;
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
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.ViewModel;

namespace WpfApp1.Pages.SettingsPages
{
    /// <summary>
    /// Логика взаимодействия для PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        public PageList()
        {
            InitializeComponent();
            DataContext = new PageListVM();
            //var currenList = Store.User.AuthorPages.ToList();
            //OwnPages.ItemsSource = currenList;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    this.NavigationService.Navigate(new AuthorPageSetting(null));
        //}
        //private void AuthorListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Получаем выбранный объект
        //    var selectedItem = OwnPages.SelectedItem as AuthorPages;

        //    if (selectedItem != null)
        //    {
        //        App.NavigationService.NavigateTo(FrameNames.MainFrame, new AuthorPage(selectedItem));
        //    }
        //}
    }
}
