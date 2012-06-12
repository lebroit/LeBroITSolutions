using LebroITSolutions.ChatModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPFChatProgramTests.ChatModelTests
{
    
    
    /// <summary>
    ///This is a test class for HostsInfoTest and is intended
    ///to contain all HostsInfoTest Unit Tests
    ///</summary>
    [TestClass]
    public class HostsInfoTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for HostsInfo Constructor
        ///</summary>
        [TestMethod]
        public void HostsInfoConstructorTest()
        {
            var target = new HostsInfo();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod]
        public void DeleteTest()
        {
            var target = new HostsInfo();
            target.CurrentHosts.Add(new Host
            {
                ChatActive = false,
                HostName = "Localhost"
            });
            target.SelectedHost.HostName = "Localhost";
            target.Delete();
            if (target.CurrentHosts.Count > 0)
            {
                target.SelectedHost.HostName = "Localhost";
                target.Delete();
            }
            Assert.AreEqual(0, target.CurrentHosts.Count);
            Assert.IsNull(target.SelectedHost.HostName);
        }
    }
}
