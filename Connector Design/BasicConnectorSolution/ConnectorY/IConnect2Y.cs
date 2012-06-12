using DomainModel;

namespace ConnectorY
{
    /// <summary>
    /// Interface as contract for the serviceagent
    /// </summary>
    public interface IConnect2Y
    {
        string WebServiceUrl { get; set; }

        Customers GetCustomers();
    }
}
