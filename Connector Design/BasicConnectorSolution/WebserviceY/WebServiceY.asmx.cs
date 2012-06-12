using System.ComponentModel;
using System.IO;
using System.Web.Services;
using BusinessLogic;
using DomainModel;

namespace WebserviceY
{
    /// <summary>
    /// WebServiceY returns customers
    /// Classic asmx webservice as a stub
    /// </summary>
    [WebService(Namespace = "http://WebServiceY.nl/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class WebServiceY : WebService
    {

        /// <summary>
        /// Gets the customers.
        /// because it's a stub you'll have to set the path to your xml
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public Customers GetCustomers()
        {
            using (var reader = new StreamReader(@"F:\DefaultCollection\Connector Design\BasicConnectorSolution\WebserviceY\App_Data\Customers.xml"))
            {
                var xml = reader.ReadToEnd();
                var ser = new XMLSerializerHelper<Customers>();
                return ser.Deserialize(xml);
            }
        }
    }
}
