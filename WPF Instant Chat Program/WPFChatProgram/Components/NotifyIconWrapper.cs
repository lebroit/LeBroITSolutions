using System;
using System.ComponentModel;
using System.Windows;
using LebroITSolutions.ChatModel;
using WPFChatProgram.Controllers;

namespace WPFChatProgram.Components
{
    /// <summary>
    /// This component is now the starting point of the application
    /// the main window which shows the controls is created, initiated, manipulated 
    /// and destroyed from this class.
    /// </summary>
    public partial class NotifyIconWrapper : Component
    {
        internal readonly MainWindow Win = new MainWindow();

#region Constructors
        public NotifyIconWrapper()
        {
            InitializeComponent();
            //Attach the event handlers
            cmdOpen.Click += CmdOpenClick;
            cmdClose.Click += CmdCloseClick;
            cmdHide.Click += CmdHideClick;
            cmdWhosOnline.Click += CmdWhosOnlineClick;
            cmdConfigSettings.Click += CmdConfigSettingsClick;
            notifyIconWrap.ShowBalloonTip(20);
            CmdConfigSettingsClick(null,null);
        }

        public NotifyIconWrapper(IContainer container)
        {
            if (Equals(container, null))
            {
                Globals.CurrentExceptionsCollection.Add(new ArgumentNullException("container"));
            }
            else
            {
                container.Add(this);
            }
            InitializeComponent();
        }

#endregion

        private void ChangeLanguage()
        {
            //Standard language is Dutch, check at starup to make the text 
            // of the commands in the contenxtmenu to English if appropraite
            if(Globals.MainUser.Language.Contains("English") || Globals.MainUser.Language.Contains("Engels"))
            {
                cmdOpen.Text = "Open Chat window?";
                cmdOpen.ToolTipText = "Opens the window to Chat.";
                cmdClose.Text = "Close all?";
                cmdClose.ToolTipText = "Click to close the program entirely.";
                cmdWhosOnline.Text = "Who's online";
                cmdWhosOnline.ToolTipText = "Check Who's online or ad an contact.";
                cmdHide.Text = "Hide all?";
                cmdHide.ToolTipText = "Hides all windows";
                cmdConfigSettings.Text = "Open configuration?";
                cmdConfigSettings.ToolTipText = "Opens the configuration window.";
            }
            else
            {
                cmdOpen.Text = "Open Chat window";
                cmdOpen.ToolTipText = "Hiermee kunt U het venster om te chatten mee openen";
                cmdClose.Text = "Afsluiten";
                cmdClose.ToolTipText = "Klik hier om het Chat programma af te sluiten.";
                cmdWhosOnline.Text = "Wie is er online?";
                cmdWhosOnline.ToolTipText = "Hiermee kunt u zien wie er allemaal online zijn.";
                cmdHide.Text = "Verberg alle schermen";
                cmdHide.ToolTipText = "Verbergt alle schermen";
                cmdConfigSettings.Text = "Configuratie";
                cmdConfigSettings.ToolTipText = "Hiermee kunt u de configuratie openen";
            }
        }

        private void CmdHideClick(object sender, EventArgs e)
        {
            Win.ScreenSelectionHidden();
        }

        /// <summary>
        /// Check the state of the window and change it
        /// to the appropriate state if necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CmdOpenClick(object sender, EventArgs e)
        {
            Win.ScreenSelector = SelectedWindow.Chat;
            Win.ScreenSelectionShow();
            MyWindowState();
            Win.Show();
            Win.Activate();
        }

        /// <summary>
        /// Shutdown the entire application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CmdCloseClick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Show's all of the configuration settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CmdConfigSettingsClick(object sender, EventArgs e)
        {
            Win.ScreenSelector = SelectedWindow.Config;
            Win.ScreenSelectionShow();
            MyWindowState();
            Win.Show();
            Win.Activate();
        }

        /// <summary>
        /// Show's who's online
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CmdWhosOnlineClick(object sender, EventArgs e)
        {
            Win.ScreenSelector = SelectedWindow.WhosOnline;
            Win.ScreenSelectionShow();
            MyWindowState();
            Win.Show();
            Win.Activate();
        }
        

#region Helper Methods
        private void MyWindowState()
        {
            if (Win.WindowState == WindowState.Minimized)
                Win.WindowState = WindowState.Normal;
            ChangeLanguage();
        }
 #endregion

    }
}
