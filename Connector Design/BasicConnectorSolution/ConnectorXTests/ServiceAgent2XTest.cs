using System.ServiceModel;
using System.ServiceModel.Channels;
using ConnectorX;
using ConnectorX.WebserviceX;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConnectorXTests
{


    /// <summary>
    ///This is a test class for ServiceAgent2XTest and is intended
    ///to contain all ServiceAgent2XTest Unit Tests
    ///</summary>
    [TestClass]
    public class ServiceAgent2XTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        /// <summary>
        ///A test for ServiceAgent2X Constructor
        ///</summary>
        [TestMethod]
        public void ServiceAgent2XConstructorTest()
        {
            var target = ServiceFactory2X.GetConnector2X("");
            Assert.IsNotNull(target);
            //evidence that the type isn't IConnext2X but the serviceagent with internal accessor
            Assert.IsFalse(target.GetType() == typeof(IConnect2X));
            Assert.IsNull(target.GetCustomer(null));
        }

        /// <summary>
        /// Test of the Iconnector instance.
        /// </summary>
        [TestMethod]
        public void InstanceTest()
        {
            var target = ServiceFactory2X.GetConnector2X("http://localhost:1965/ServiceX.svc");
            var customer = target.GetCustomer("ALFKI");

            TestCustomer(customer);
        }

        /// <summary>
        /// Test of the ServiceClient constructor (generated proxy).
        /// </summary>
        [TestMethod]
        public void ServiceClientConstructorTest()
        {
            var target = new ServiceXClient("BasicHttpBinding_IServiceX");
            var customer = target.GetData("ALFKI");
            TestCustomer(customer);

            target = new ServiceXClient("BasicHttpBinding_IServiceX", "http://localhost:1965/ServiceX.svc");
            customer = target.GetData("ALFKI");
            TestCustomer(customer);

            var remoteAddress = new EndpointAddress("http://localhost:1965/ServiceX.svc");
            target = new ServiceXClient("BasicHttpBinding_IServiceX", remoteAddress);
            customer = target.GetData("ALFKI");
            TestCustomer(customer);

            target = new ServiceXClient(NewBinding(), remoteAddress);
            customer = target.GetData("ALFKI");
            TestCustomer(customer);
        }

        #region Test helper methods

        /// <summary>
        /// Creates a new binding.
        /// </summary>
        /// <returns></returns>
        public Binding NewBinding()
        {
            return new BasicHttpBinding
            {
                Name = "BasicHttpBinding_IServiceX",
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                Security = { Mode = BasicHttpSecurityMode.None }
            };
        }

        /// <summary>
        /// Tests the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void TestCustomer(Customer customer)
        {
            Assert.IsNotNull(customer);
            var customerRef = CreateCustomer();
            Assert.AreEqual(customer.CustomerId, customerRef.CustomerId);
            Assert.AreEqual(customer.Address, customerRef.Address);
            Assert.AreEqual(customer.City, customerRef.City);
            Assert.AreEqual(customer.Country, customerRef.Country);
            Assert.AreEqual(customer.ContactName, customerRef.ContactName);
            Assert.AreEqual(customer.CompanyName, customerRef.CompanyName);
            Assert.AreEqual(customer.Phone, customerRef.Phone);
            Assert.AreEqual(customer.PostalCode, customerRef.PostalCode);
            Assert.AreEqual(customer.Fax, customerRef.Fax);
            Assert.AreEqual(customer.Region, customerRef.Region);
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <returns></returns>
        public Customer CreateCustomer()
        {
            return new Customer
                       {
                           CustomerId = "ALFKI",
                           Address = "Obere Str. 57",
                           City = "Berlin",
                           ContactName = "Maria Anders",
                           PostalCode = "12209",
                           CompanyName = "Alfreds Futterkiste",
                           Country = "Germany",
                           Phone = "030-0074321",
                           Fax = "030-0076545",
                           Region = string.Empty
                       };
        }

        #endregion
    }
}
