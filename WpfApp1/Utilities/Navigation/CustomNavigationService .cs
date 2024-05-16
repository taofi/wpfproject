using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp1.Models.Navigation
{
    public class CustomNavigationService : INavigationService
    {
        private Dictionary<FrameNames, Frame> frames = new Dictionary<FrameNames, Frame>();
       
        public event EventHandler NavigationStateChanged;

        private void OnNavigationStateChanged()
        {
            NavigationStateChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RegisterFrame(FrameNames frameKey, Frame frame)
        {
            if (frames.ContainsKey(frameKey))
            {
                frames.Remove(frameKey);
            }
            if (frame == null) throw new ArgumentNullException(nameof(frame));
            frames[frameKey] = frame;
            frames[frameKey].LoadCompleted += Frame_LoadCompleted;
            
        }

        public void UnregisterFrame(FrameNames frameKey)
        {
            if (frames.ContainsKey(frameKey))
            {
                frames[frameKey].LoadCompleted -= Frame_LoadCompleted;
                frames.Remove(frameKey);
            }
        }

        public void NavigateTo(FrameNames frameKey, Page page)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");

            frames[frameKey].Navigate(page);
            OnNavigationStateChanged();
        }

        public void GoBack(FrameNames frameKey)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");

            if (frames[frameKey].CanGoBack)
                frames[frameKey].GoBack();
            OnNavigationStateChanged();
        }
        private void Frame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            OnNavigationStateChanged();
        }
        public bool CanGoBack(FrameNames frameKey)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");

            return frames[frameKey].CanGoBack;
        }
        public void GoForward(FrameNames frameKey)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");

            if (frames[frameKey].CanGoForward)
                frames[frameKey].GoForward();
            OnNavigationStateChanged();
        }

        public bool CanGoForward(FrameNames frameKey)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");

            return frames[frameKey].CanGoForward;
        }
        public void ClearNavigationStack(FrameNames frameKey)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");
            while (frames[frameKey].NavigationService.CanGoBack)
            {
                frames[frameKey].NavigationService.RemoveBackEntry();
            }
            if (frames[frameKey].NavigationService.CanGoBack)
            {
                frames[frameKey].NavigationService.RemoveBackEntry();
            }
            OnNavigationStateChanged();

        }
        private void HandleNavigatedWithoutHistory(object sender, NavigationEventArgs e)
        {
            Frame frame = sender as Frame;
            frame.Navigated -= HandleNavigatedWithoutHistory;

            if (frame.NavigationService.CanGoBack)
            {
                frame.NavigationService.RemoveBackEntry();
            }
            OnNavigationStateChanged();

        }
        public void NavigateWithoutHistory(FrameNames frameKey, Page page)
        {
            if (!frames.ContainsKey(frameKey))
                throw new KeyNotFoundException($"No frame registered with key: {frameKey}");
            frames[frameKey].Navigated += HandleNavigatedWithoutHistory;
            frames[frameKey].Navigate(page);
        }


    }

}
