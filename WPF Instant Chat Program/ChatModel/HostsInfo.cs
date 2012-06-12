using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using LebroITSolutions.ChatModel.Properties;

namespace LebroITSolutions.ChatModel
{
    public class HostsInfo
    {
        private HostsCollection hostsCollection;
        /// <summary>
        /// Prop that holds te host info about the hosts
        /// that are available
        /// </summary>
        public HostsCollection CurrentHosts
        {
            get { return hostsCollection ?? (hostsCollection = new HostsCollection()); }
        }

#region Save the current hosts list

        public void Save()
        {
            Settings.Default.HostsList = CurrentHosts;
            Settings.Default.Save();
        }

#endregion

#region Delete/Remove a host from the list

        private Host hostToDelete;
        /// <summary>
        /// Holds the Selected Host that can be used
        /// </summary>
        public Host SelectedHost 
        { 
            get { return hostToDelete ?? (hostToDelete = new Host()); }
            set { hostToDelete = value; }
        }

        public HostsInfo()
        {
            foreach (var host in Settings.Default.HostsList)
            {
                host.IPAdress = null;
                CurrentHosts.Add(host);

            }
        }

        /// <summary>
        /// Removes the 
        /// </summary>
        public void Delete()
        {
            if(SelectedHost.HostName != null)
            {
                CurrentHosts.Remove(CurrentHosts.FirstOrDefault(
                             h => h.HostName == SelectedHost.HostName)) ;
                Save();
                //reset the Host to delete for a next round
                SelectedHost = null;
            }
        }

 #endregion
    }

    /// <summary>
    /// just a wrapper class for the generic list setting
    /// </summary>
    public class HostsCollection : ObservableCollection<Host>
    {
        
    }

    public class Host
    {
        public string HostName { get; set; }
        public string IPAdress { get; set; }
        public bool ChatActive { get; set; }
    }
}
