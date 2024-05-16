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

namespace WpfApp1.User_Controls
{
    public partial class IconButton : UserControl
    {
        public IconButton()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty NormalImageProperty = DependencyProperty.Register(
           "NormalImage", typeof(ImageSource), typeof(IconButton), new PropertyMetadata(null));

        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        public static readonly DependencyProperty HoverImageProperty = DependencyProperty.Register(
            "HoverImage", typeof(ImageSource), typeof(IconButton), new PropertyMetadata(null));

        public ImageSource HoverImage
        {
            get { return (ImageSource)GetValue(HoverImageProperty); }
            set { SetValue(HoverImageProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(IconButton), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(IconButton), new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register(
         "ButtonWidth", typeof(double), typeof(IconButton), new PropertyMetadata(double.NaN));

        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        public static readonly DependencyProperty ButtonHeightProperty = DependencyProperty.Register(
            "ButtonHeight", typeof(double), typeof(IconButton), new PropertyMetadata(double.NaN));

        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        public static readonly DependencyProperty IsCanGoBackProperty = DependencyProperty.Register(
            "IsCanGoBack", typeof(bool), typeof(IconButton), new PropertyMetadata(true));

        public bool IsCanGoBack
        {
            get { return (bool)GetValue(IsCanGoBackProperty); }
            set { SetValue(IsCanGoBackProperty, value); }
        }
    }
}
