using System.Windows;
using System.Windows.Input;
using LebroITSolutions.ChatModel;
using WPFChatProgram.Components;
using WPFChatProgram.Controllers;

namespace WPFChatProgram.Controls
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration
    {

#region Constructor
        
        /// <summary>
        /// Construct for this view, initializing components
        /// and controls
        /// </summary>
        public Configuration()
        {
            InitializeComponent();
            IpAddressesCmb.ItemsSource = Globals.ListWithIPAdresses;
            Keyboard.Focus(ContinueBtn);
        }

#endregion

#region Event Handlers

        /// <summary>
        /// the alias textbox is directly wired up to the checkbox in the XAML, 
        /// username isn't, because it's inversed, therefore doing it overhere
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameOrAliasChkChecked(object sender, RoutedEventArgs e)
        {
            UserNameTxt.IsEnabled = (!((ConfigurationController)DataContext).MainUser.UserNameOrAlias);
        }

        /// <summary>
        /// Handles the Continue button event
        /// Opens the WhosOnline usercontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ContinueClicked(object sender, RoutedEventArgs e)
        {
            ((NotifyIconWrapper)Application.Current.Properties["Component"]).CmdWhosOnlineClick(sender, e);
        }

        /// <summary>
        /// When the languageselection is changed, calls the controller to load
        /// another language resource dictionary so all the text is adjusted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageCmbSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (LanguageCmb.SelectedValue == null) return;
            LanguageCmb.ItemsSource = ((ConfigurationController)DataContext).LanguageUsed(LanguageCmb.SelectedValue.ToString());
            LanguageCmb.SelectedValue = LanguageCmb.Items[0];
        }

#endregion
    }
}
