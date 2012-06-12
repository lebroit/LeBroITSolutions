using LebroITSolutions.ChatModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPFChatProgramTests.ChatModelTests
{
    
    
    /// <summary>
    ///This is a test class for HostTest and is intended
    ///to contain all HostTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HostTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        /// <summary>
        ///A test for Host Constructor
        ///</summary>
        [TestMethod()]
        public void HostConstructorTest()
        {
            var target = new Host();
            Assert.IsNotNull(target);
        }

        
    }
}
