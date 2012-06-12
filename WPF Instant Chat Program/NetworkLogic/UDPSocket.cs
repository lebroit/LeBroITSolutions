using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace LebroITSolutions.NetworkLogic
{
    /// <summary>
    /// This is a Buffer class incorperating the UDP transport protocol from system.net 
    /// </summary>
    public sealed class UDPSocket : IDisposable
    {
#region Class members

        private int onlinePort;
        public UdpClient Udpsock;
        public UdpClient UdpsocketOnlineSignal;
        private readonly List<IPAddress> bsender;
        public IPEndPoint RemoteIPendpoint = new IPEndPoint(IPAddress.IPv6Any, 0);
        public bool UDPSocketIsInit;

#endregion

#region Constructor
        
        public UDPSocket()
        {
            Udpsock = new UdpClient();
            UdpsocketOnlineSignal = new UdpClient();
            bsender = new List<IPAddress>();
        }

#endregion

#region Methods

        /// <summary>
        /// Inits the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public bool Init(int port)
        {
            try
            {
                if (port > 0) UDPSocketIsInit = true;
                Udpsock = new UdpClient(port) {EnableBroadcast = true};
                //the online signal has a port just above the message broadcast
                UdpsocketOnlineSignal = new UdpClient(port + 1) { EnableBroadcast = true };
                onlinePort = port + 1;

                return true;
            }
            catch (SocketException)
            {
                throw;
            }
            catch(Exception)
            {
                //general exceptions aren't interesting so just return false
                return false;
            }
        }

        

        ///<summary>
        ///Allows an attempt to free resources and perform other 
        ///cleanup operations before the object is reclaimed by garbage collection.
        ///</summary>
        ///
        ~UDPSocket()
        {
           Dispose(false); 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Equals(Udpsock, null))
                {
                    Udpsock.Close();
                    Udpsock = null;
                }

                if (!Equals(UdpsocketOnlineSignal))
                {
                    UdpsocketOnlineSignal.Close();
                    UdpsocketOnlineSignal = null;
                }
            }
        }

        #endregion

#region Send & Receive Methods

        /// <summary>
        /// Sends the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public bool Send(MemoryStream data, IPEndPoint address)
        {
            try
            {
                //if more then 1 bite is send return true
                return Udpsock.Send(data.ToArray(), data.ToArray().Length, address) > 0;
            }
            catch (SocketException)
            {
                throw;
            }
            catch (Exception)
            {
                //general exceptions aren't interesting so just return false
                return false;
            }
        }


        /// <summary>
        /// Sends the online signal.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public bool SendOnlineSignal(byte []data, IPAddress address)
        {
            try
            {
                var endPoint = new IPEndPoint(address, onlinePort);
                //if more then 1 bite is send return true
                return UdpsocketOnlineSignal.Send(data, data.Length, endPoint) > 0;
            }
            catch(SocketException)
            {
                throw;
            }
            catch (Exception)
            {
                //general exceptions aren't interesting so just return false
                return false;
            }
        }

        /// <summary>
        /// Checks if the IP address is valid.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public bool IPAddressIsValid(IPAddress address)
        {
            try
            {
                var ipHostEntry = Dns.GetHostEntry(address);
                if (!Equals(ipHostEntry, null)) return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Starts receiving messages.
        /// </summary>
        public byte[] StartReceiving()
        {
            if (Udpsock.Available > 0)
            {

                try
                {
                    byte[] recievedbytes = Udpsock.Receive(ref RemoteIPendpoint);
                    if (!bsender.Contains(RemoteIPendpoint.Address)) bsender.Add(RemoteIPendpoint.Address);
                    return recievedbytes;
                }
                catch (SocketException)
                {
                    throw new SocketException(RemoteIPendpoint.Port);
                }
                catch (Exception)
                {
                    //general exceptions aren't interesting so just return null
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Receives the chat app. is active signal.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public byte[] ReceiveChatActiveSignal(IPAddress address)
        {
            if (UdpsocketOnlineSignal.Available > 0)
            {
                var endPoint = new IPEndPoint(address, onlinePort);
                try
                {
                    byte[] recievedbytes = UdpsocketOnlineSignal.Receive(ref endPoint);
                    if (!bsender.Contains(endPoint.Address)) bsender.Add(endPoint.Address);
                    return recievedbytes;
                }
                catch (SocketException)
                {
                    throw new SocketException(endPoint.Port);
                }
                catch (Exception)
                {
                    //general exceptions aren't interesting so just return null
                    return null;
                }
            }
            return null;
        }

#endregion
    }
}

