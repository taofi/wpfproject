using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Models.Navigation;
using WpfApp1.Pages.SettingsPages;

namespace WpfApp1.ViewModel
{
    internal class CreateAuthorVM 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateCommand { get; private set; }

        public CreateAuthorVM()
        {
            CreateCommand = new RelayCommand(_ => Create());
        }
        private void Create()
        {
            App.NavigationService.NavigateWithoutHistory(FrameNames.MainFrame, new AuthorPageSetting(null));
        }
    }
}
