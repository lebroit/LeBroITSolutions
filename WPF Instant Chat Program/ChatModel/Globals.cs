using System;
using System.Collections.ObjectModel;
using System.Net;
using LebroITSolutions.NetworkLogic;

namespace LebroITSolutions.ChatModel
{
    public static class Globals
    {

        #region Properties and attributes

        /// <summary>
        /// Gets or sets a value indicating whether [message is send].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [message is send]; otherwise, <c>false</c>.
        /// </value>
        public static bool MessageIsSend { get; set; }

        private static UDPSocket udpSock;
        /// <summary>
        /// Gets or sets the UDP socket.
        /// </summary>
        /// <value>
        /// The UDP sock.
        /// </value>
        [CLSCompliant(false)]
        public static UDPSocket UdpSock
        {
            get { return udpSock ?? (udpSock = new UDPSocket()); }
            set { udpSock = value; }
        }

        /// <summary>
        /// Gets or sets the default port.
        /// </summary>
        /// <value>
        /// The default port.
        /// </value>
        public static int DefaultPort
        {
            get { return Convert.ToInt32(MainUser.DefaultPort); }
            set { MainUser.DefaultPort = value.ToString(); }
        }

        private static ObservableCollection<string> hostList;
        public static ObservableCollection<string> HostList
        {
            get { return hostList = hostList ?? new ObservableCollection<string>(); }
        }

        private static ObservableCollection<string> listWithIPAdresses;
        public static ObservableCollection<string> ListWithIPAdresses
        {
            get { return listWithIPAdresses ?? (listWithIPAdresses = new ObservableCollection<string>()); }
        }

        private static ChatUserInfo mainUser;
        public static ChatUserInfo MainUser
        {
            get { return mainUser ?? (mainUser = new ChatUserInfo()); }
            set { mainUser = value; }
        }

        private static HostsInfo hostInfo;
        public static HostsInfo HostInfo
        {
            get { return hostInfo ?? (hostInfo = new HostsInfo()); }
            set { hostInfo = value; }
        }

        private static Collection<Exception> currentExceptionsCollection;
        /// <summary>
        /// Gets or sets the current exceptions.
        /// </summary>
        /// <value>
        /// The current exceptions.
        /// </value>
        public static Collection<Exception> CurrentExceptionsCollection
        {
            get
            {
                return currentExceptionsCollection ??
                      (currentExceptionsCollection = new Collection<Exception>());
            }
        }
        #endregion

        /// <summary>
        /// Updates the hosts.
        /// </summary>
        public static void UpdateHosts()
        {
            HostList.Clear();
            HostList.Add(MainUser.UserName);

            if (HostInfo.CurrentHosts != null && hostList != null)
            {
                Model2NetworkFunctions.GetIPContactHosts();
            }
        }

        /// <summary>
        /// Deletes the users.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static bool DeleteUsers(string userName)
        {
            HostList.Remove(userName);
            return true;
        }
    }
}
