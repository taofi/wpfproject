using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.Models.Navigation
{
    public interface INavigationService
    {
        void RegisterFrame(FrameNames frameKey, Frame frame);
        void UnregisterFrame(FrameNames frameKey);
        void NavigateTo(FrameNames frameKey, Page page);
        void GoBack(FrameNames frameKey);
        bool CanGoBack(FrameNames frameKey);
        void GoForward(FrameNames frameKey);
        bool CanGoForward(FrameNames frameKey);
    }

}
