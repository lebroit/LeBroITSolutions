namespace ConnectorY
{
    /// <summary>
    /// Serviceagent factory class
    /// returns a instance of the serviceagent
    /// </summary>
    public class ServiceFactory2Y
    {
        /// <summary>
        /// Gets the serviceagent as an 
        /// implementation of the IConnect2Y interface
        /// for implementing Morphisme
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static IConnect2Y GetConnector2Y(string url)
        {
            var serviceAgent = new ServiceAgentY();
            if (!string.IsNullOrEmpty(url)) serviceAgent.WebServiceUrl = url;
            return serviceAgent;
        }
    }
}
