using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using LebroITSolutions.ChatModel;
using LebroITSolutions.Common.Infrastructure;

namespace WPFChatProgram.Controllers
{
    public class WhosOnlineController : MainController
    {

#region Who's Online Propterties and Attributes

        /// <summary>
        /// This prop keeps trac of the selected user
        /// </summary>
        public string UserSelected { get; set; }

        private string hostNameToAdd;
        /// <summary>
        /// Represents the name of the Host to add
        /// to the currentusers
        /// </summary>
        public string HostNameToAdd 
        { 
            get { return hostNameToAdd; } 
            set { hostNameToAdd = value; 
                  OnPropertyChanged(new PropertyChangedEventArgs("HostNameToAdd")); } 
        }

        /// <summary>
        /// Prop to interface with the globals list
        /// </summary>
        public ObservableCollection<string> CurrentHosts
        {
            get{ return Globals.HostList;}
        }

// ReSharper disable UnaccessedField.Local
        private ObservableCollection<string> displayHosts;
// ReSharper restore UnaccessedField.Local
        /// <summary>
        /// Created the display text for the gui
        /// </summary>
        public ObservableCollection<string> DisplayHosts
        {
            get { return (displayHosts = CreateDisplayText()); }
            set { displayHosts = value; OnPropertyChanged(new PropertyChangedEventArgs("DisplayHosts")); }
        }

        /// <summary>
        /// Holds the Host that can be deleted and is
        /// wired up with the globa host to delete
        /// </summary>
        // omdat de display een string en geen Host object toont
        public string HostToDelete
        {
            get { return Globals.HostInfo.SelectedHost.HostName; }
            set
            {
                Globals.HostInfo.SelectedHost.HostName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HostToDelete"));
            }
        }

#endregion

        public void Refresh()
        {
            DisplayHosts = CreateDisplayText();
        }

#region Commands

        #region Save Command
        /// <summary>
        /// Gets the save hosts command.
        /// </summary>
        public ICommand SaveHostsCommand
        {
            get { return new RelayCommand(SaveExecute, CanSaveHostsExecute); }
        }

        /// <summary>
        /// Execute the Save command.
        /// </summary>
        private void SaveExecute()
        {
            //Check is the host already exists
            var hostRef = Globals.HostInfo.CurrentHosts.FirstOrDefault(
                ch => ch.HostName == HostNameToAdd);

            if (!Equals(hostRef, null))
            {
                MessageBox.Show(MessageHostExists(), "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            //split hostname and ipadress if given with the HostNameToAdd variable
            var ipAdress = string.Empty;
            if (HostNameToAdd.Contains(","))
            {
                var strRef = HostNameToAdd.Replace(" ", "");
                var strArr = strRef.Split(',');
                IPAddress test;
                if(IPAddress.TryParse(strArr[1],out  test))
                {
                    if (Globals.UdpSock.IPAddressIsValid(test))
                    {
                        ipAdress = strArr[1];}
                }

                HostNameToAdd = strArr[0];
            }

        //Add the new hostname to the hostslist and hostinfo
            if (!Equals(Globals.HostInfo.CurrentHosts, null))
                Globals.HostInfo.CurrentHosts.Add(new Host{HostName = HostNameToAdd, IPAdress = ipAdress});
            RemoveLocalhostFromList();
            DisplayHosts = CreateDisplayText();
            Globals.HostInfo.Save();
            HostNameToAdd = null;
        }
        
        /// <summary>
        /// Give s message that the host exists.
        /// </summary>
        /// <returns></returns>
        private string MessageHostExists()
        {
            return  MainUser.Language == "English"
                    ? "Computername is already in the list" 
                    : "Computernaam staat al in de lijst";
        }

        /// <summary>
        /// Removes the default localhost from list.
        /// </summary>
        private void RemoveLocalhostFromList()
        {
            var host = Globals.HostInfo.CurrentHosts.FirstOrDefault(h => h.HostName == "Localhost");
            if(host != null && Globals.HostInfo.CurrentHosts.Contains(host))
                Globals.HostInfo.CurrentHosts.Remove(host);
        }

        /// <summary>
        /// Determines whether this instance [can execute the save hosts command].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can execute the save hosts command]; otherwise, <c>false</c>.
        /// </returns>
        Boolean CanSaveHostsExecute()
        {
            return (!ReferenceEquals(Globals.HostInfo.CurrentHosts, null));
        }

        #endregion

        #region Delete Command
        /// <summary>
        /// Gets the delete hosts command.
        /// </summary>
        public ICommand DeleteHostsCommand
        {
            get { return new RelayCommand(DeleteExecute, CanDeleteHostsExecute); }
        }

        /// <summary>
        /// Deletes the execute.
        /// </summary>
        public void DeleteExecute()
        {
            if (!CanDeleteHostsExecute()) return;
            ResolveHostName();
            Globals.HostInfo.Delete();
            DisplayHosts = CreateDisplayText();
        }

        /// <summary>
        /// Resolves the name of the host.
        /// </summary>
        private void ResolveHostName()
        {
            if(!HostToDelete.Contains(" "))return;
            var temp = HostToDelete.Split(' ');
            HostToDelete = Regex.Replace(temp[1], @"\s", "");
        }
        /// <summary>
        /// Determines whether this instance [can the delete hosts execute].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can the delete hosts execute]; otherwise, <c>false</c>.
        /// </returns>
        static Boolean CanDeleteHostsExecute()
        {
            return (Globals.HostInfo.CurrentHosts.Count > 0);
        }

        #endregion

        #region Start chat command

        /// <summary>
        /// Gets the startchat command.
        /// </summary>
        public ICommand StartChatCommand
        {
            get { return new RelayCommand(StartChatExecute, CanStartChatExecute); }
        }

        /// <summary>
        /// Starts the chat execute.
        /// </summary>
        public void StartChatExecute()
        {
            if (string.IsNullOrEmpty(HostToDelete) || 
                !HostToDelete.Contains("Computer:") ||
                HostToDelete.Contains(MainUser.UserName)) return;

            ResolveHostName();
            Globals.HostInfo.SelectedHost.HostName = HostToDelete;
            Globals.HostInfo.SelectedHost.IPAdress = 
                Globals.HostInfo.CurrentHosts.FirstOrDefault(
                    ch => ch.HostName == HostToDelete).IPAdress;
        }

        /// <summary>
        /// Determines whether this instance [can start chat execute].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can start chat execute]; otherwise, <c>false</c>.
        /// </returns>
        static Boolean CanStartChatExecute()
        {
            return (Globals.HostInfo.CurrentHosts.Count >= 1);
        }

        #endregion

#endregion
        
    }
}
