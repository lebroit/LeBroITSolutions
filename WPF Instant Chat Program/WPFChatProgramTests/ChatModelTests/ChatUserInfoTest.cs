using LebroITSolutions.ChatModel;
using LebroITSolutions.ChatModel.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace WPFChatProgramTests.ChatModelTests
{
    
    
    /// <summary>
    ///This is a test class for ChatUserInfoTest and is intended
    ///to contain all ChatUserInfoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChatUserInfoTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private ChatUserInfo target;

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            target = new ChatUserInfo();
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            target = null;
        }
        
        #endregion


        /// <summary>
        ///A test for ChatUserInfo Constructor
        ///</summary>
        [TestMethod()]
        public void ChatUserInfoConstructorTest()
        {
            var expectedUserInfo = new ChatUserInfo();
            Assert.AreEqual(target.GetType(), expectedUserInfo.GetType());
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            var expected = new ChatUserInfo();
            target.Load();

            if (target.Alias != null)Assert.AreNotEqual(expected.Alias, target.Alias);
            if (target.HostName != null) Assert.AreNotEqual(expected.HostName, target.HostName);
            if (target.Language != null) Assert.AreNotEqual(expected.Language, target.Language);
            if (target.SubnetMask != null) Assert.AreNotEqual(expected.SubnetMask, target.SubnetMask);
            if (target.TextColor != null) Assert.AreNotEqual(expected.TextColor, target.TextColor);
            if (target.UserName != null) Assert.AreNotEqual(expected.UserName, target.UserName);
            if (target.DefaultPort != null) Assert.AreNotEqual(expected.DefaultPort, target.DefaultPort);
            if (target.FontName != null) Assert.AreNotEqual(expected.FontName, target.FontName);

            Settings.Default.UserName = "Onzin";
            Settings.Default.Alias = "Alias";
            Settings.Default.UserNameOrAlias = true;
            target.Load();
            Assert.AreEqual(target.Alias, "Alias");
            Assert.AreEqual(target.UserName, Environment.UserName);

            Settings.Default.UserName = "User";
            Settings.Default.UserNameOrAlias = false;
            Settings.Default.TextColor = null;
            target.Load();
            Assert.AreEqual(target.UserName, "User");
            Assert.AreEqual(target.TextColor, "Black");
            Assert.IsNull(target.Alias);

            Settings.Default.TextColor = "Red";
            target.Load();
            Assert.AreEqual(target.TextColor, "Red");
            
            foreach (var property in target.GetType().GetProperties())
            {
                if(!property.CanRead) return;
                expected.GetType().GetProperty(property.Name).SetValue(expected, property.GetValue(target, null),null);//Member(member.Name).SetValue(expected, );
            }

            Assert.AreEqual(expected.Alias, target.Alias);
            Assert.AreEqual(expected.Hidden, target.Hidden);
            Assert.AreEqual(expected.HostName, target.HostName);
            Assert.AreEqual(expected.Ignore, target.Ignore);
            Assert.AreEqual(expected.IsMainUser, target.IsMainUser);
            Assert.AreEqual(expected.Language, target.Language);
            Assert.AreEqual(expected.MainUserHasChanges, target.MainUserHasChanges);
            Assert.AreEqual(expected.SubnetMask, target.SubnetMask);
            Assert.AreEqual(expected.TextColor, target.TextColor);
            Assert.AreEqual(expected.UserName, target.UserName);
            Assert.AreEqual(expected.UserNameOrAlias, target.UserNameOrAlias);
            Assert.AreEqual(expected.DefaultPort, target.DefaultPort);
            Assert.AreEqual(expected.FontName, target.FontName);
            Assert.AreEqual(expected.FontSize, target.FontSize);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            target.IpAddress = "168.1.1.1";
            target.Hidden = false;
            target.IsMainUser = true;
            target.UserName = "testuser";
            target.Language = "koeterwaals";
            target.Save();
            Assert.AreEqual(target.IpAddress, Settings.Default.IPAddress);
            Assert.IsFalse(target.Hidden);
            Assert.IsTrue(target.IsMainUser);
            Assert.AreEqual(Settings.Default.UserName, "testuser");
            Assert.AreEqual(Settings.Default.Language, "koeterwaals");
        }
        
        
    }
}
