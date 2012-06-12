using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LebroITSolutions.ChatModel;
using LebroITSolutions.Common.Infrastructure;
using WPFChatProgram.Components;

namespace WPFChatProgram.Controllers
{
    /// <summary>
    /// The Controller for the main screen
    /// </summary>
    public class MainController : INotifyPropertyChanged
    {

#region Properties and Attributes

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the main user.
        /// </summary>
        /// <value>
        /// The main user.
        /// </value>
        public ChatUserInfo MainUser
        {
            get { return Globals.MainUser; }
            set { Globals.MainUser = value; OnPropertyChanged(new PropertyChangedEventArgs("MainUser")); }
        }


        /// <summary>
        /// Gets or sets the default port org value.
        /// </summary>
        /// <value>
        /// The default port org value.
        /// </value>
        public string DefaultPortOrgValue { get; set; }

#endregion

        public MainController()
        {
            DefaultPortOrgValue = MainUser.DefaultPort;
        }


        /// <summary>
        /// Handling of the onpropertychanged event.
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        

 #region Commands

        #region Save Command
        /// <summary>
        /// Gets the save command.
        /// </summary>
        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveExecute, CanSaveExecute); }
        }

        /// <summary>
        /// Execute the Save command.
        /// </summary>
        private void SaveExecute()
        {
            if (!CanSaveExecute()) return;
            MainUser.MainUserHasChanges = false;
            MainUser.Save();
            if(! Equals(DefaultPortOrgValue, MainUser.DefaultPort))
            {
                ((ChatController)((NotifyIconWrapper)Application.Current.Properties["Component"]).Win.Chat.DataContext).Initialize();
                DefaultPortOrgValue = MainUser.DefaultPort;
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute the save command].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can execute the save command]; otherwise, <c>false</c>.
        /// </returns>
        Boolean CanSaveExecute()
        {
            return (MainUser != null);
        }

        #endregion

#endregion

#region User Display Text
        /// <summary>
        /// Creates the displaytext with current (Main)User
        /// and all the hosts that are known
        /// </summary>
        /// <returns>string with text to display</returns>
        public ObservableCollection<string> CreateDisplayText()
        {
            //get the mainuser from the config file
            if (MainUser.UserName == null) MainUser.Load();
            var whosOnline = new List<string>
                                 {
                                     string.Format("{0}{1}Computer: {2}{3}IP: {4}",
                                                   MainUser.UserName, Environment.NewLine,
                                                   MainUser.HostName, Environment.NewLine,
                                                   MainUser.IpAddress)
                                 };
            Globals.UpdateHosts();
            string hostDisplay;
            // remove each host with hostname and ip equals null
            RemoveAllEmptyHosts();
            // if the ip address is null the host is offline
            if (Globals.HostInfo.CurrentHosts.Contains(
                Globals.HostInfo.CurrentHosts.FirstOrDefault(
                ch => string.IsNullOrEmpty(ch.IPAdress))))
            {
                hostDisplay = MainUser.Language == "English"
                                     ? "Computer: {0}{1} Is currently offline."
                                     : "Computer: {0}{1} Is momenteel niet bereikbaar.";
                whosOnline.AddRange(Globals.HostInfo.CurrentHosts.ToList().FindAll(
                    ch => string.IsNullOrEmpty(ch.IPAdress)).Select(
                    // ReSharper disable AccessToModifiedClosure
                    host => string.Format(hostDisplay, host.HostName, Environment.NewLine, host.IPAdress)));
                // ReSharper restore AccessToModifiedClosure
            }

            hostDisplay = MainUser.Language == "English"
                            ? "Computer: {0}{1} With IP address: {2} is online."
                            : "Computer: {0}{1} Met IP adres: {2} is online.";

            whosOnline.AddRange(Globals.HostInfo.CurrentHosts.ToList().FindAll(
                    ch => !string.IsNullOrEmpty(ch.IPAdress)).Select(
                    host => string.Format(hostDisplay, host.HostName,
                                          Environment.NewLine, host.IPAdress)));

            return new ObservableCollection<string>(whosOnline);
        }

        /// <summary>
        /// Removes all empty hosts.
        /// </summary>
        private void RemoveAllEmptyHosts()
        {
            foreach (var host in
                    Globals.HostInfo.CurrentHosts.ToList().FindAll(
                        host => host.HostName == null && host.IPAdress == null))
            {
                Globals.HostInfo.CurrentHosts.Remove(host);
            }


        }

#endregion
    }
}
