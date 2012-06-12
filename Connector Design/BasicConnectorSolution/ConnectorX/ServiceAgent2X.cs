using System;
using System.ServiceModel;
using DomainModel;

namespace ConnectorX
{
    /// <summary>
    /// Internal ServiceAgent to morph the implementation of the connector
    /// </summary>
    internal class ServiceAgent2X : IConnect2X
    {
        #region Implementation of IConnect2X
        /// <summary>
        /// Gets or sets the web service URL.
        /// Optional, if not set the URL of the configuration is used
        /// </summary>
        /// <value>
        /// The web service URL.
        /// </value>
        public string WebServiceUrl { get; set; }

        /// <summary>
        /// Gets the customer based on the ID.
        /// </summary>
        /// <param name="customerID">The customer ID.</param>
        /// <returns></returns>
        public Customer GetCustomer(string customerID)
        {
            using (var serviceX = new WebserviceX.ServiceXClient())
            {
                if (!String.IsNullOrEmpty(WebServiceUrl))
            {
                serviceX.Endpoint.Address = new EndpointAddress(WebServiceUrl);
            }
            return serviceX.GetData(customerID);
            }
        }

        #endregion
    }
}
