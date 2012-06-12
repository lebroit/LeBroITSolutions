using ConnectorX;
using ConnectorY;
using DomainModel;

namespace ApplicationA.Controllers
{
    /// <summary>
    /// The controller class for the views
    /// </summary>
    public class MasterDetailController
    {
#region Properties
        private Customers customers;
        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public Customers Customers
        {
            get
            {
                if (Equals(customers, null))
                {
                    customers = GetCustomers();
                } return customers;
            }

        }
        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public Customer Customer { get; set; }
        /// <summary>
        /// Gets or sets the URL Y described by the interface
        /// </summary>
        /// <value>
        /// The URL Y.
        /// </value>
        public string UrlY { get; set; }
        /// <summary>
        /// Gets or sets the URL X described by the interface
        /// </summary>
        /// <value>
        /// The URL X.
        /// </value>
        public string UrlX { get; set; }

#endregion
        /// <summary>
        /// Gets the customers, usage of IConnect2Y interface
        /// </summary>
        /// <returns></returns>
        public Customers GetCustomers()
        {
            IConnect2Y iserviceY = ServiceFactory2Y.GetConnector2Y(UrlY);
            return iserviceY.GetCustomers();
        }

        /// <summary>
        /// Gets the customer, usage of IConnect2X interface
        /// </summary>
        /// <param name="customerID">The customer ID.</param>
        public void GetCustomer(string customerID)
        {
            IConnect2X iserviceX = ServiceFactory2X.GetConnector2X(UrlX);
            Customer = iserviceX.GetCustomer(customerID);
        }

       
    }
}
