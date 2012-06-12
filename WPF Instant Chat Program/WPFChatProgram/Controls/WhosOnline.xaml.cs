using System.Collections.Generic;
using System.Windows;
using WPFChatProgram.Components;

namespace WPFChatProgram.Controls
{
    /// <summary>
    /// Interaction logic for WhosOnline.xaml
    /// </summary>
    public partial class WhosOnline
    {

#region Constructor
        /// <summary>
        /// Construct for this view, initializing components
        /// and controls
        /// </summary>
        public WhosOnline()
        {
            InitializeComponent();
        }

#endregion

#region Event handlers

        /// <summary>
        /// Starts the chat window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void StartChatMenuItemClick(object sender, RoutedEventArgs e)
        {
            ((NotifyIconWrapper)Application.Current.Properties["Component"]).CmdOpenClick(sender, e);
        }

#endregion
        
    }
}
