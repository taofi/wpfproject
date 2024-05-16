using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WpfApp1.Models;
using WpfApp1.Utilities;

namespace WpfApp1.Converters
{
    internal class ImageSourceConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (string.IsNullOrEmpty(path))
            {
                path = "pack://application:,,,/Resources/img/profile.png"; 
            }
            if(!Validater.IsValidImageFormat(path))
                path = "pack://application:,,,/Resources/img/file.png";
            return new BitmapImage(new Uri($"{path}", UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
