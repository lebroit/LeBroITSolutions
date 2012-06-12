using System.ServiceModel;
using System.ServiceModel.Channels;
using ConnectorY;
using ConnectorY.WebserviceY;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customer = DomainModel.Customer;

namespace ConnectorYTests
{
    
    
    /// <summary>
    ///This is a test class for ServiceAgentYTest and is intended
    ///to contain all ServiceAgentYTest Unit Tests
    ///</summary>
    [TestClass]
    public class ServiceAgentYTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        /// <summary>
        ///A test for ServiceAgentY Constructor
        ///</summary>
        [TestMethod]
        public void ServiceAgentYConstructorTest()
        {
            var target = ServiceFactory2Y.GetConnector2Y(string.Empty);
            Assert.IsNotNull(target);
            //evidence that the type isn't IConnext2X but the serviceagent with internal accessor
            Assert.IsFalse(target.GetType() == typeof(IConnect2Y));
            Assert.IsNotNull(target.GetCustomers());
        }

        /// <summary>
        /// Test of the Iconnector instance.
        /// </summary>
        [TestMethod]
        public void InstanceTest()
        {
            var target = ServiceFactory2Y.GetConnector2Y("http://localhost:2316/WebServiceY.asmx");
            var customers = target.GetCustomers();

            TestCustomers(customers);
        }

        /// <summary>
        /// Test of the ServiceClient constructor (generated proxy).
        /// </summary>
        [TestMethod]
        public void ServiceClientConstructorTest()
        {
            var target = new WebServiceYSoapClient("WebServiceYSoap");
            var customers = Conversions.ConvertCustomers(target.GetCustomers());
            TestCustomers(customers);

            target = new WebServiceYSoapClient("WebServiceYSoap", "http://localhost:2316/WebServiceY.asmx");
            customers = Conversions.ConvertCustomers(target.GetCustomers());
            TestCustomers(customers);

            var remoteAddress = new EndpointAddress("http://localhost:2316/WebServiceY.asmx");
            target = new WebServiceYSoapClient("WebServiceYSoap", remoteAddress);
            customers = Conversions.ConvertCustomers(target.GetCustomers());
            TestCustomers(customers);

            target = new WebServiceYSoapClient(NewBinding(), remoteAddress);
            customers = Conversions.ConvertCustomers(target.GetCustomers());
            TestCustomers(customers);

            var webCustomers = target.GetCustomers();
            Assert.IsNotNull(webCustomers[0].ExtensionData);
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
                Name = "WebServiceYSoap",
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                Security = { Mode = BasicHttpSecurityMode.None }
            };
        }

        /// <summary>
        /// Tests the customer.
        /// </summary>
        /// <param name="customers">The customer.</param>
        public void TestCustomers(Customers customers)
        {
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Count == 91);
            var customer = CreateCustomer();
            var customerRef = customers.Find(cust => cust.CustomerId ==  customer.CustomerId);

            Assert.IsTrue(customers.Contains(customerRef));

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
