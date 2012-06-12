using DomainModel;

namespace ConnectorX
{
    /// <summary>
    /// Interface as contract for the serviceagent
    /// </summary>
    public interface IConnect2X
    {
        /// <summary>
        /// Gets or sets the optional web service URL.
        /// </summary>
        /// <value>
        /// The web service URL.
        /// </value>
        string WebServiceUrl { get; set; }

        /// <summary>
        /// Gets the customer and returns it based on the ID.
        /// </summary>
        /// <param name="customerID">The customer ID.</param>
        /// <returns></returns>
        Customer GetCustomer(string customerID);
    }
}
