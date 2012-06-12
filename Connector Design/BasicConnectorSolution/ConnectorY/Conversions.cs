using System.Collections.Generic;
using System.Linq;
using ConnectorY.WebserviceY;

namespace ConnectorY
{
    /// <summary>
    /// Class that handles conversions of object types
    /// </summary>
    public class Conversions
    {
        /// <summary>
        /// Converts the webservice customers in to Domainmodel customers
        /// Here it's shown a advantage of using objects, with Linq you can select
        /// the range of objects fullfilling certain demands in one handle.
        /// </summary>
        /// <param name="customersY">The customers Y.</param>
        /// <returns></returns>
        public static DomainModel.Customers ConvertCustomers(IEnumerable<Customer> customersY)
        {
            var customers = new DomainModel.Customers();
            customers.AddRange(customersY.Select(customerY => new DomainModel.Customer
                                                                  {
                                                                      Address = customerY.Adres, 
                                                                      City = customerY.Woonplaats, 
                                                                      CompanyName = customerY.Bedrijfsnaam, 
                                                                      ContactName = customerY.Contact, 
                                                                      Country = customerY.Land, 
                                                                      CustomerId = customerY.CustomerID, 
                                                                      Fax = customerY.Fax, 
                                                                      Phone = customerY.Telefoon, 
                                                                      PostalCode = customerY.Postcode, 
                                                                      Region = customerY.Provincie
                                                                  }));

            return customers;
        }
    }
}
