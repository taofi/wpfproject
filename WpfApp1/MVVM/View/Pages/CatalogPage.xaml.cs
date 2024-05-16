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
using WpfApp1.ViewModel;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
            DataContext = new CatalogVM();
        }
        //private void AuthorListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Получаем выбранный объект
        //    var selectedItem = AuthorPages.SelectedItem as AuthorPages;

        //    if (selectedItem != null)
        //    {
        //        this.NavigationService.Navigate(new AuthorPage(selectedItem));
        //    }
        //}

    }
}
