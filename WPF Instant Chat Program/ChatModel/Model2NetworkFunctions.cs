using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows;

namespace LebroITSolutions.ChatModel
{
    public static class Model2NetworkFunctions
    {
        #region Class members

        private static readonly Regex IsNumber = new Regex(@"^\d+.+$");

        #endregion


        /// <summary>
        /// Returns a list with the local IPAdresses
        /// </summary>
        /// <returns>List with IP's</returns>
        public static List<string> GetListOfIPAdresses()
        {
            var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            var ipList = new List<string>();
            try
            {
                ipList.AddRange(from netInterface in netInterfaces
                                from address in netInterface.GetIPProperties().UnicastAddresses
                                select address.Address.ToString());
                ipList.RemoveAll(m => !IsNumber.Match(m).Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message));
            }
            return ipList;
        }

        /// <summary>
        /// Resolves ip addresses based on the hostnames and add them to
        /// the currenthosts property
        /// </summary>
        public static void GetIPContactHosts()
        {
            if (Globals.HostInfo.CurrentHosts.Count == (Globals.HostList.Count - 1)) return;
            
            foreach (var host in Globals.HostInfo.CurrentHosts.Where(h => string.IsNullOrEmpty(h.IPAdress)))
            {
                try
                {
                    var addrFiltered = ReturnFilteredIPAddress(host.HostName);
                    
                    Globals.HostList.Add(host.HostName);
                    Globals.HostInfo.CurrentHosts.First(h => h.HostName == 
// ReSharper disable AccessToModifiedClosure
                                                        host.HostName).IPAdress =
// ReSharper restore AccessToModifiedClosure
                                                        addrFiltered[0].ToString();
                }
                //If the ip for the hostname couldn't be resolved
                //give the ex.message to the host.HostName property
                catch(Exception ex)
                {
                    var hostEx = new Exception(string.Format("Host: {0}, Message: {1}", host.HostName, ex.Message));
                    Globals.CurrentExceptionsCollection.Add(hostEx);
                }
            }
            //Check if the current IPAddress needs to be resolved
            if(string.IsNullOrWhiteSpace(Globals.MainUser.IpAddress))
            {
                Globals.MainUser.IpAddress = ReturnFilteredIPAddress(Dns.GetHostName())[0].ToString();
            }
        }

        private static List<IPAddress> ReturnFilteredIPAddress(string hostName)
        {
            IPAddress[] addr = Dns.GetHostEntry(hostName).AddressList;
            return addr.Where(a => IsNumber.Match(a.ToString()).Success).ToList();
        }
    }
}
