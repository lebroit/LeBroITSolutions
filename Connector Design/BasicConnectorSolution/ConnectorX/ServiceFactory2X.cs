namespace ConnectorX
{
    /// <summary>
    /// Serviceagent factory class
    /// returns a instance of the serviceagent
    /// </summary>
    public class ServiceFactory2X
    {
        /// <summary>
        /// Gets the serviceagent as an 
        /// implementation of the IConnect2Y interface
        /// for implementing Morphisme
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static IConnect2X GetConnector2X(string url)
        {
            var serviceAgent = new ServiceAgent2X();
            if (!string.IsNullOrEmpty(url)) serviceAgent.WebServiceUrl = url;
            return serviceAgent;
        }
    }
}
