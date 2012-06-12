using System;
using System.Net.Sockets;
using System.Text;
using LebroITSolutions.NetworkLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;

namespace WPFChatProgramTests
{


    /// <summary>
    ///This is a test class for UDPSocketTest and is intended
    ///to contain all UDPSocketTest Unit Tests
    ///</summary>
    [TestClass]
    public class UDPSocketTest
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
        ///A test for UDPSocket Constructor
        ///</summary>
        [TestMethod]
        public void UDPSocketConstructorTest()
        {
            var target = new UDPSocket();
            Assert.AreEqual(target.Udpsock.GetType(), typeof(UdpClient));
            Assert.AreEqual(target.UdpsocketOnlineSignal.GetType(), typeof(UdpClient));
            target.Dispose();
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod]
        public void DisposeTest()
        {
            var target = new UDPSocket();
            target.Dispose();
            Assert.IsNull(target.Udpsock);
            Assert.IsNull(target.UdpsocketOnlineSignal);
            GC.WaitForPendingFinalizers();
            Assert.IsNotNull(target);
        }


        /// <summary>
        ///A test for IPAddressIsValid
        ///</summary>
        [TestMethod]
        public void IPAddressIsValidTest()
        {
            var target = new UDPSocket();
            var address = new IPAddress(0x2414188f);
            bool actual = target.IPAddressIsValid(address);
            Assert.AreEqual(false, actual);
            address = Dns.GetHostAddresses("localhost")[0];
            actual = target.IPAddressIsValid(address);
            Assert.AreEqual(true, actual);
            address = IPAddress.Parse("192.168.1.112");
            actual = target.IPAddressIsValid(address);
            Assert.AreEqual(false, actual);
            target.Dispose();
        }

        /// <summary>
        ///A test for Init
        ///</summary>
        [TestMethod]
        public void InitTest()
        {
            var target = new UDPSocket();
            var port = 8080;
            const bool expected = true;
            var actual = target.Init(port);
            Assert.AreEqual(expected, actual);
            target = new UDPSocket();
            port = -8080;
            Assert.IsFalse(target.Init(port));
            target.Dispose();

        }
    }
}
