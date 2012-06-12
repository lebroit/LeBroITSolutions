using System.IO;
using DomainModel;
using BusinessLogic;

namespace WebserviceX
{
    /// <summary>
    /// This class is the service stub to test the connector against
    /// </summary>
   public class ServiceX : IServiceX
    {
        #region Implementation of IServiceX

        /// <summary>
        /// Gets the data and returns a customer object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Customer GetData(string value)
        {
            using (var reader = new StreamReader(@"F:\DefaultCollection\Connector Design\BasicConnectorSolution\WebserviceX\App_Data\Customers.xml"))
            {
                var xml = reader.ReadToEnd();
                var ser = new XMLSerializerHelper<Customers>();
                return ser.Deserialize(xml).Find(c => c.CustomerId == value);
            }
        }

        #endregion
    }
}
