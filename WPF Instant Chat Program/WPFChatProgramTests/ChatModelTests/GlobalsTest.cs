using System.Net;
using System.Net.Sockets;
using LebroITSolutions.ChatModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using LebroITSolutions.NetworkLogic;

namespace WPFChatProgramTests.ChatModelTests
{


    /// <summary>
    ///This is a test class for GlobalsTest and is intended
    ///to contain all GlobalsTest Unit Tests
    ///</summary>
    [TestClass]
    public class GlobalsTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for DeleteUsers
        ///</summary>
        [TestMethod]
        public void DeleteUsersTest()
        {
            Globals.HostList.Clear();
            Globals.HostList.Add("user");
            const string userName = "user";
            Assert.AreEqual(Globals.HostList[0], userName);
            bool actual = Globals.DeleteUsers(userName);
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for UpdateHosts
        ///</summary>
        [TestMethod]
        public void UpdateHostsTest()
        {
            Globals.MainUser.UserName = "user";
            Globals.MainUser.IpAddress = null;
            Globals.UpdateHosts();
            Assert.IsNotNull(Globals.MainUser.IpAddress);
        }


        /// <summary>
        ///A test for DefaultPort
        ///</summary>
        [TestMethod]
        public void DefaultPortTest()
        {
            var info = new ChatUserInfo();
            info.Load();
            //Something went wrong with loading the settings
            if (Convert.ToInt32(info.DefaultPort) == 0)
                info.DefaultPort = "8080";

            Globals.DefaultPort = Convert.ToInt32(info.DefaultPort);
            var actual = Globals.DefaultPort;
            Assert.AreEqual(8080, actual);
        }

        /// <summary>
        ///A test for HostInfo
        ///</summary>
        [TestMethod]
        public void HostInfoTest()
        {
            var expected = new HostsInfo();
            Globals.HostInfo = expected;
            HostsInfo actual = Globals.HostInfo;
            if (expected.CurrentHosts.Count == 0) return;
            Assert.AreEqual(expected.CurrentHosts[0].HostName, actual.CurrentHosts[0].HostName);
            Assert.AreEqual(expected.CurrentHosts[0].ChatActive, actual.CurrentHosts[0].ChatActive);
            Assert.IsNull(actual.CurrentHosts[0].IPAdress);
        }


        /// <summary>
        ///A test for ListWithIPAdresses
        ///</summary>
        [TestMethod]
        public void ListWithIPAdressesTest()
        {
            Globals.ListWithIPAdresses.Add("192.168.1.1");
            ObservableCollection<string> actual = Globals.ListWithIPAdresses;
            Assert.AreEqual(actual[0], "192.168.1.1");
        }

        /// <summary>
        ///A test for MainUser
        ///</summary>
        [TestMethod]
        public void MainUserTest()
        {
            var expected = new ChatUserInfo
                                        {
                                            DefaultPort = "8080",
                                            HostName = "local",
                                            Hidden = false,
                                            Language = "Dutch"
                                        };
            Globals.MainUser = null;
            var actual = Globals.MainUser;
            Assert.IsNotNull(actual);
            Globals.MainUser = expected;
            actual = Globals.MainUser;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UDPSock
        ///</summary>
        [TestMethod]
        public void UDPSockTest()
        {
            var expected = new UDPSocket
                            {
                                RemoteIPendpoint = new IPEndPoint(IPAddress.Parse("192.168.1.100"), 9999),
                                Udpsock = new UdpClient(9999),
                                UDPSocketIsInit = false
                            };

            Globals.UdpSock = null;
            var actual = Globals.UdpSock;
            Assert.IsNotNull(actual);
            Globals.UdpSock = expected;
            actual = Globals.UdpSock;
            Assert.AreEqual(expected, actual);
            expected.Dispose();
        }
    }
}
