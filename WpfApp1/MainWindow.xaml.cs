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
using WpfApp1.Models.Navigation;
using WpfApp1.Pages;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {  
        public MainWindow()
        {
            InitializeComponent();
            App.NavigationService.RegisterFrame(FrameNames.MainFrame, MainFrame);
            DataContext = new MainModel();
        }
    }
}
