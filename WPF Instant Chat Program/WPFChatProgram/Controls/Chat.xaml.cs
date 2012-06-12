using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using LebroITSolutions.ChatModel;
using WPFChatProgram.Controllers;
using WpfApplication = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace WPFChatProgram.Controls
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat
    {

#region Properties and Attributes
        
        private String item;
        private readonly DispatcherTimer dispatcherTimer;

#endregion
        

#region Constructor and Binding

        /// <summary>
        /// Initializes a new instance of the <see cref="Chat"/> class.
        /// </summary>
        public Chat()
        {
            InitializeComponent();
            FontCmb.ItemsSource = Fonts.SystemFontFamilies;
            // Initialize the dispatch timer
            dispatcherTimer = new DispatcherTimer
                                  {
                                      Interval = new TimeSpan(0, 0, 1), 
                                      IsEnabled = true
                                  };
            dispatcherTimer.Tick += DispatcherTimerTick;
        }
        
        #endregion


#region Text editing

        /// <summary>
        /// Adds the picture to the rich textbox.
        /// </summary>
        /// <param name="imagelocation">The imagelocation.</param>
        private void AddPicture(string imagelocation)
        {
            var bitmap = new BitmapImage(new Uri(imagelocation));
            var image = new Image {Source = bitmap, Width = 35};
            var uiContainer = new InlineUIContainer(image);
            var tp = txtMessageToSend.CaretPosition;
            if (tp.Paragraph != null) tp.Paragraph.Inlines.Add(uiContainer);
        }

        /// <summary>
        /// Copies the content between the RTF´s and sends the new text to the recipients
        /// </summary>
        private void CopyContentBetweenRTFsAndSend()
        {
            ((ChatController)DataContext).SendChatMessageExecute();
            for (int i = 0; i < txtMessageToSend.Document.Blocks.Count; i++)
            {
                txtMessageSendReceive.Document.Blocks.Add(txtMessageToSend.Document.Blocks.FirstBlock);
            }
            
        }

#endregion



#region Events
        
        #region User Interface Events

        #region Button Click Events

        /// <summary>
        /// Searches the icons.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchIcons(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var openDialog = new OpenFileDialog { CheckPathExists = true, DefaultExt = "ICO", InitialDirectory = Application.StartupPath + @"\Icons" };

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrEmpty(openDialog.FileName))
                    {
                        var fi = new FileInfo(openDialog.FileName);
                        item = fi.FullName;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Selecteer een afbeelding.", "Error",
                                                       System.Windows.MessageBoxButton.OK,
                                                       System.Windows.MessageBoxImage.Error);
                    }
                    AddPicture(@item);
                }

            }
            catch (Exception ex)
            {
                Globals.CurrentExceptionsCollection.Add(ex);
         
                var errorMessage = WpfApplication.Current.FindResource("IconLoadError") ?? "unknown error";
                MessageBox.Show(errorMessage.ToString());
            }
        }
        
        #endregion

        /// <summary>
        /// Handles the Enterkey pressed event to move up the text to the
        /// rtb for send and received messages
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void EnterKeyPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    CopyContentBetweenRTFsAndSend();
                }
                catch (InvalidOperationException iex)
                {
                    MessageBox.Show(iex.Message);
                }
                finally
                {
                    // To make sure we start again on line 1
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// the FontcolorCMB selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void FontColorCmbSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(Equals(FontColorCmb.SelectedValue, null) ||
               Globals.MainUser.TextColor == FontColorCmb.SelectedValue.ToString()) return;
            Globals.MainUser.TextColor = FontColorCmb.SelectedValue.ToString();
            Globals.MainUser.Save();
        }

        /// <summary>
        /// the FontsizeCMB selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void FontSizeCmbSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(Equals(FontSizeCmb.SelectedValue, null)||
               Globals.MainUser.FontSize == Convert.ToDouble(FontSizeCmb.SelectedValue)) return;
            Globals.MainUser.FontSize = Convert.ToDouble(FontSizeCmb.SelectedValue);
            Globals.MainUser.Save();
        }

        /// <summary>
        /// the FontCMB selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void FontCmbSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(Equals(FontCmb.SelectedValue, null) ||
               Globals.MainUser.FontName == FontCmb.SelectedValue.ToString()) return;
            Globals.MainUser.FontName = FontCmb.SelectedValue.ToString();
            Globals.MainUser.Save();
        }

        #endregion

        #region Background Events


        /// <summary>
        /// Dispatches the timer tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            ((ChatController) DataContext).ReceiveChatMessageExecute();
            ((ChatController)DataContext).SendOnlineSignal();
            ChatHostActive.BackgroundRangeColor = ((ChatController) DataContext).ChatContactActiveColor;
        }

        #endregion

#endregion
    }
}