using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using LebroITSolutions.ChatModel;
using LebroITSolutions.Common.Infrastructure;
using LebroITSolutions.NetworkLogic;

namespace WPFChatProgram.Controllers
{
    /// <summary>
    /// A Controller class for the chat window
    /// </summary>
    public class ChatController : MainController
    {

#region Chat Properties and Attributes

        #region DocumentSendReceive Property

        private FlowDocument documentSend;
        /// <summary>
        /// Gets or sets the document to send and receive.
        /// </summary>
        /// <value>
        /// The document to send and receive.
        /// </value>
        public FlowDocument DocumentSend
        {
            get { return documentSend ?? (documentSend = new FlowDocument()); }
            set { documentSend = value; 
                  OnPropertyChanged(new PropertyChangedEventArgs("DocumentSend")); }
        }

        private FlowDocument documentReceived;
        /// <summary>
        /// Gets or sets the document to send and receive.
        /// </summary>
        /// <value>
        /// The document to send and receive.
        /// </value>
        public FlowDocument DocumentSendReceived
        {
            get { return documentReceived ?? (documentReceived = new FlowDocument()); }
            set { documentReceived = value;
                  OnPropertyChanged(new PropertyChangedEventArgs("DocumentSendReceived")); }
        }

        #endregion

        private Collection<double> fontSize;

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        /// <returns>The size of the text in the <see cref="T:System.Windows.Controls.Control"/>. 
        /// The default is <see cref="P:System.Windows.SystemFonts.MessageFontSize"/>. 
        /// The font size must be a positive number.</returns>
        public Collection<double> FontSizeRtb
        {
            get { return fontSize ?? (fontSize = new Collection<double>()); }
        }

        public Color ChatContactActiveColor { get; set; }

        private string chatContactActiveText;
        public string ChatContactActiveText
        {
            get { return chatContactActiveText; }
            set 
            { 
                chatContactActiveText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ChatContactActiveText"));
            }
        }

        #endregion

#region Constructor

        public ChatController()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the whos online screen.
        /// </summary>
        internal void Initialize()
        {
            if(Globals.DefaultPort <= 0)MainUser.Load();
            Globals.UdpSock = new UDPSocket();
            try
            {
                Globals.UdpSock.Init(Globals.DefaultPort);
            }
            catch (SocketException socketEx)
            {
                if (Globals.MainUser.Language == "English")
                {
                    MessageBox.Show(string.Format("Fatal Configuration Error: {0} {1} Restart to resolve!", socketEx.Message, Environment.NewLine));
                }
                else if (Globals.MainUser.Language == "Nederlands")
                {
                    MessageBox.Show(string.Format("Fatale Configuratie Fout: {0} {1} herstart om het op te lossen!", socketEx.Message, Environment.NewLine));
                }
                //add the exception to the collection
                Globals.CurrentExceptionsCollection.Add(socketEx);
            }
            // fill the fontsizelist with numbers
            FillFontSizeList();
        }

        /// <summary>
        /// Fills the font size list.
        /// </summary>
        private void FillFontSizeList()
        {
            for (var i = 1; i <= 40; i++)
            {
                FontSizeRtb.Add(i);
            }
        }

#endregion

#region Send/Receive 

        #region chat message

        /// <summary>
        /// Executes the chat send message .
        /// </summary>
        public void SendChatMessageExecute()
        {
            AddTimeStampAndUserName();
            using (var stream = new MemoryStream())
            {
                try
                {
                    XamlWriter.Save(DocumentSend, stream);
                    Globals.MessageIsSend =
                        Globals.UdpSock.Send(stream,
                        new IPEndPoint(
                            IPAddress.Parse(Globals.HostInfo.SelectedHost.IPAdress),
                            Convert.ToInt32(Globals.MainUser.DefaultPort)));
                }
                catch (SocketException sockEx)
                {
                    Globals.CurrentExceptionsCollection.Add(sockEx);
                }
                catch(SecurityException sucEx)
                {
                    Globals.CurrentExceptionsCollection.Add(sucEx);
                }
                catch(Exception ex)
                {
                    Globals.CurrentExceptionsCollection.Add(ex);
                }
                
            }
        }

        /// <summary>
        /// Adds the username and timestamp to the message.
        /// </summary>
        private void AddTimeStampAndUserName()
        {
            //Creating a new run with name and timestamp
            var run = new Run(string.Format("{0}: {1} \r",
                              Globals.MainUser.UserName,
                              DateTime.Now.ToShortTimeString()))
            {
                FontFamily = new FontFamily("courier"),
                Foreground = Brushes.Gray
            };
            //Insert the run before the firstinline
            if (!(DocumentSend.Blocks.FirstBlock is Paragraph)) return;

            //explicit set the font properties so they are 
            //transferred with the message
            ((Paragraph) DocumentSend.Blocks.FirstBlock).Inlines.FirstInline.FontFamily =
                new FontFamily(Globals.MainUser.FontName);
            ((Paragraph)DocumentSend.Blocks.FirstBlock).Inlines.FirstInline.FontSize =
                Globals.MainUser.FontSize.GetValueOrDefault(16);
            var color = System.Drawing.Color.FromName(Globals.MainUser.TextColor);
            ((Paragraph)DocumentSend.Blocks.FirstBlock).Inlines.FirstInline.Foreground =
                new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            //Insert timestamp
            ((Paragraph)DocumentSend.Blocks.FirstBlock).Inlines.InsertBefore(
                ((Paragraph)DocumentSend.Blocks.FirstBlock).Inlines.FirstInline, run);
        }

        /// <summary>
        /// Executes the chat send message .
        /// </summary>
        public void ReceiveChatMessageExecute()
        {
            FlowDocument receivedDoc;
            if(Globals.UdpSock.Udpsock.Available < 1) return;
            using (var stream = new MemoryStream())
            {
                var bytes = Globals.UdpSock.StartReceiving();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                receivedDoc = XamlReader.Load(stream) as FlowDocument;
                if (!Equals(receivedDoc, null) && !Equals(receivedDoc.Blocks.FirstBlock,null))
                {
                    DocumentSendReceived.Blocks.Add(receivedDoc.Blocks.FirstBlock);
                }
            }
        }

        #endregion

        #region online signal
        /// <summary>
        /// Sends the online signal.
        /// </summary>
        public void SendOnlineSignal()
        {
            Globals.UpdateHosts();
            var message = Message(MainUser.HostName, MainUser.IpAddress);
            var encoding=new UTF8Encoding();
            foreach (var host in Globals.HostInfo.CurrentHosts)
            {
                if(string.IsNullOrEmpty(host.IPAdress))return;
                Globals.UdpSock.SendOnlineSignal(encoding.GetBytes(message), 
                    IPAddress.Parse(host.IPAdress));
                host.ChatActive = ReceiveOnlineSignal();
                SetAppropriateTextAndColor();
            }
        }

        public bool ReceiveOnlineSignal()
        {
            //no IpAdress or the udpsocket not available returns false immediately
            if (Globals.HostInfo.SelectedHost.IPAdress == null||
                Globals.UdpSock.UdpsocketOnlineSignal.Available <= 0) return false;

            var chatActiveBytes = Globals.UdpSock.ReceiveChatActiveSignal(IPAddress.Parse(
                    Globals.HostInfo.SelectedHost.IPAdress));
            string text;
            using (var stream = new MemoryStream(chatActiveBytes))
            {
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }
            return text.ToLower() == Message(
                Globals.HostInfo.SelectedHost.HostName, 
                Globals.HostInfo.SelectedHost.IPAdress).ToLower();
        }

        private static string Message(string hostName, string ipAdress)
        {
            return string.Format("Host: {0} with IP: {1} Chat is active",
                                        hostName,
                                        ipAdress);
        }

        private void SetAppropriateTextAndColor()
        {
            var inactive = Equals(Globals.HostInfo.CurrentHosts.FirstOrDefault(
                host => Equals(host.HostName, Globals.HostInfo.SelectedHost.HostName) &&
                        Equals(host.ChatActive, true)), null);
            ChatContactActiveColor = inactive ? Colors.Red : Colors.Green;
            ChatContactActiveText = inactive ? "Contact inactive" : "Contact active";
        }

        #endregion

#endregion

#region Commands

        #region
        /// <summary>
        /// Gets the save command.
        /// </summary>
        public ICommand EraseFlowDocumentCommand
        {
            get { return new RelayCommand(EraseFlowDocumenExecute, CanEraseFlowDocumenExecute); }
        }

        /// <summary>
        /// Executea the Save command.
        /// </summary>
        private void EraseFlowDocumenExecute()
        {
            if (!CanEraseFlowDocumenExecute()) return;
            DocumentSendReceived.Blocks.Clear();
        }

        /// <summary>
        /// Determines whether this instance [can execute the save command].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can execute the save command]; otherwise, <c>false</c>.
        /// </returns>
        Boolean CanEraseFlowDocumenExecute()
        {
            return (DocumentSendReceived != null);
        }

        #endregion


#endregion
    }
}
