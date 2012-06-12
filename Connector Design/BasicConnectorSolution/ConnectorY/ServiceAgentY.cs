using System;
using System.ServiceModel;
using ConnectorY.WebserviceY;
using DomainModel;

namespace ConnectorY
{
    /// <summary>
    /// Internal ServiceAgent to morph the implementation of the connector
    /// </summary>
    internal class ServiceAgentY : IConnect2Y
    {
        #region Implementation of IConnectorY

        /// <summary>
        /// Gets or sets the web service URL.
        /// Optional, if not set the URL of the configuration is used
        /// </summary>
        /// <value>
        /// The web service URL.
        /// </value>
        public string WebServiceUrl{ get; set; }


        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public Customers GetCustomers()
        {
            var serviceY = new WebServiceYSoapClient();
            if (!String.IsNullOrEmpty(WebServiceUrl))
            {
                serviceY.Endpoint.Address = new EndpointAddress(WebServiceUrl);
            }
            return Conversions.ConvertCustomers(serviceY.GetCustomers());
        }

        #endregion
    }
}
