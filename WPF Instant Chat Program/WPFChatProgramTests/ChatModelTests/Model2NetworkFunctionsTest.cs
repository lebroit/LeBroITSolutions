using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using LebroITSolutions.ChatModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace WPFChatProgramTests.ChatModelTests
{
    
    
    /// <summary>
    ///This is a test class for Model2NetworkFunctionsTest and is intended
    ///to contain all Model2NetworkFunctionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class Model2NetworkFunctionsTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

  
        /// <summary>
        ///A test for GetIPContactHosts
        ///</summary>
        [TestMethod]
        public void GetIPContactHostsTest()
        {
            var host = Dns.GetHostEntry("localhost");
            Globals.HostInfo.CurrentHosts.Add(new Host
                                                  {
                                                      ChatActive = false,
                                                      HostName = host.HostName,
                                                      IPAdress = host.AddressList[1].ToString()
                                                  });
            var reference = host.AddressList[1].ToString();
            Model2NetworkFunctions.GetIPContactHosts();
            Assert.AreNotEqual(reference, Globals.MainUser.IpAddress);
            Globals.HostInfo.CurrentHosts.Clear();
            Globals.MainUser.IpAddress = null;
            if (Globals.HostInfo.CurrentHosts.Count != (Globals.HostList.Count - 1) && 
                Globals.HostList.Count == 0)
            {
                Globals.HostList.Add("host");
            }
            Model2NetworkFunctions.GetIPContactHosts();
            if (string.IsNullOrEmpty(Globals.MainUser.IpAddress))
                Assert.IsNull(Globals.MainUser.IpAddress);
        }

        /// <summary>
        ///A test for GetIPContactHosts
        ///</summary>
        [TestMethod]
        public void GetIPContactHostsExceptionTest()
        {
            if (Globals.HostInfo.CurrentHosts.Count == 0)
            {
                var host = Dns.GetHostEntry("localhost");
                Globals.HostInfo.CurrentHosts.Add(new Host
                {
                    ChatActive = false,
                    HostName = host.HostName,
                    IPAdress = null
                });
            }
            Globals.HostInfo.CurrentHosts[0].HostName = "dit kan niet";
          
            Model2NetworkFunctions.GetIPContactHosts();
            Assert.IsNotNull(Globals.CurrentExceptionsCollection);
            if (Globals.CurrentExceptionsCollection.Count > 0 && Globals.CurrentExceptionsCollection[0].Message.Contains("onbekend"))
                Assert.AreEqual(Globals.CurrentExceptionsCollection[0].Message, "Host: dit kan niet, Message: Host is onbekend");
            else if (Globals.CurrentExceptionsCollection.Count > 0 && Globals.CurrentExceptionsCollection[0].Message.Contains("known"))
                Assert.AreEqual(Globals.CurrentExceptionsCollection[0].Message, "Host: dit kan niet, Message: No such host is known");
        }

        /// <summary>B
        ///A test for GetListOfIPAdresses
        ///</summary>
        [TestMethod]
        public void GetListOfIPAdressesTest()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            List<string> actual = Model2NetworkFunctions.GetListOfIPAdresses();
            var isNumber = new Regex(@"^\d+.+$");
            var expected = (from @interface in networkInterfaces
                            from address in @interface.GetIPProperties().UnicastAddresses
                            where isNumber.Match(address.Address.ToString()).Success
                            select address.Address.ToString()).ToList();
            for (var i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue(actual.Contains(expected[i]));
            }
        }
    }
}
