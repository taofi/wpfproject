using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace WpfApp1.Pages.SettingsPages
{
    /// <summary>
    /// Логика взаимодействия для AuthorPageSetting.xaml
    /// </summary>
    public partial class AuthorPageSetting : Page
    {
        public AuthorPageSetting(AuthorPages page)
        {
            InitializeComponent();
            DataContext = new AuthorPageSettingVM(page);
        }

        //private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        string selectedFileName = openFileDialog.FileName;
        //        _page.PageIcon = selectedFileName;
        //        BitmapImage bitmapImage = new BitmapImage(new Uri(_page.PageIcon));
        //        Icon.Source = bitmapImage;
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e) 
        //{
        //    try
        //    {
        //        if (_page.AuthorPage_id == 0)
        //        {
        //            _page.Owner = Store.User.User_id;
        //            BaseModel.AuthorPages.Create(_page);
        //        }
        //        BaseModel.Save();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        StringBuilder error = new StringBuilder();
        //        foreach (var eve in ex.EntityValidationErrors)
        //        {
        //            error.AppendLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                error.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
        //            }
        //        }
        //        MessageBox.Show(error.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    this.NavigationService.Navigate(new PageList());
        //}


    }
}
