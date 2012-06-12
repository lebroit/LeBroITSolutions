using System;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;
using WPFChatProgram.Controllers;

namespace WPFChatProgram
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow
    {

#region Properties

        /// <summary>
        /// Gets or sets the screen selector.
        /// </summary>
        /// <value>
        /// The screen selector.
        /// </value>
        public SelectedWindow ScreenSelector { get; set; }

#endregion
        public MainWindow()
        {
            InitializeComponent();
        }

#region MainWindow Events

        private void MouseLeftClick(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            WindowState = WindowState.Minimized;
            ShowInTaskbar = false;
        }

#endregion


#region ScreenBehavior

        /// <summary>
        /// Selction method to show the appropiate usercontrol
        /// </summary>
        public void ScreenSelectionShow()
        {
            switch (ScreenSelector)
            {
                case SelectedWindow.WhosOnline:
                    WhosOnline.Visibility = Visibility.Visible;
                    ((WhosOnlineController)WhosOnline.DataContext).Refresh();
                    Chat.Visibility = Visibility.Hidden;
                    ConfigurationSettings.Visibility = Visibility.Hidden;
                    ((Storyboard)Application.Current.Resources["showWhosOnlineWindow"]).Begin(this);
                    break;
                case SelectedWindow.Chat:
                    Chat.Visibility = Visibility.Visible;
                    WhosOnline.Visibility = Visibility.Hidden;
                    ConfigurationSettings.Visibility = Visibility.Hidden;
                    ((Storyboard)Application.Current.Resources["showChatWindow"]).Begin(this);
                    break;
                case SelectedWindow.Config:
                    ConfigurationSettings.Visibility = Visibility.Visible;
                    WhosOnline.Visibility = Visibility.Hidden;
                    Chat.Visibility = Visibility.Hidden;
                    ((Storyboard)Application.Current.Resources["showConfigurationWindow"]).Begin(this);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// /// Selction method to hide the appropiate usercontrol
        /// </summary>
        public void ScreenSelectionHidden()
        {
            Storyboard storyboard;
            switch (ScreenSelector)
            {
                case SelectedWindow.WhosOnline:
                    storyboard = (Storyboard)Application.Current.Resources["hideWhosOnlineWindow"];
                    WireUpStoryboard(storyboard);
                    break;
                case SelectedWindow.Chat:
                    storyboard = (Storyboard)Application.Current.Resources["hideChatWindow"];
                    WireUpStoryboard(storyboard);
                    break;
                case SelectedWindow.Config:
                    storyboard = (Storyboard) Application.Current.Resources["hideConfigurationWindow"];
                    WireUpStoryboard(storyboard);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// We have to use a clone to enable wiring up a method to the completed event
        /// otherwise the isfrozen state of the storyboard stays true and the method
        /// to the completed event can't be attached (exception thrown)
        /// </summary>
        /// <param name="storyboard"></param>
        private void WireUpStoryboard(Storyboard storyboard)
        {
            storyboard = storyboard.Clone();
            storyboard.Begin(this);
            storyboard.Completed += MainWindowStoryboardCompleted;  
        }

        /// <summary>
        /// The method (delegate) that's wired up to the 
        /// storyboard.completed event (clone)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowStoryboardCompleted(object sender, EventArgs e)
        {
            if (WhosOnline.Visibility == Visibility.Visible) WhosOnline.Visibility = Visibility.Hidden;
            if (Chat.Visibility == Visibility.Visible) Chat.Visibility = Visibility.Hidden;
            if (ConfigurationSettings.Visibility == Visibility.Visible) ConfigurationSettings.Visibility = Visibility.Hidden;
        }

#endregion

        private void MinimizeWindowsClick(object sender, RoutedEventArgs e)
        {
            ScreenSelectionHidden();
        }

        private void CloseChatClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
