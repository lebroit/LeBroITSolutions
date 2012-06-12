using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DomainModel
{
    [Serializable]    
    public class Customers : List<Customer>
    {
    }

    [Serializable]
    public class Customer
    {
        [XmlElement("CustomerID", typeof(string))]
        public string CustomerId { get; set; }

        [XmlElement("Bedrijfsnaam", typeof(string))]
        public string CompanyName { get; set; }

        [XmlElement("Contact", typeof(string))]
        public string ContactName { get; set; }

        [XmlElement("Adres", typeof(string))]
        public string Address { get; set; }

        [XmlElement("Woonplaats", typeof(string))]
        public string City { get; set; }

        [XmlElement("Provincie", typeof(string))]
        public string Region { get; set; }

        [XmlElement("Postcode", typeof(string))]
        public string PostalCode { get; set; }

        [XmlElement("Land", typeof(string))]
        public string Country { get; set; }

        [XmlElement("Telefoon", typeof(string))]
        public string Phone { get; set; }

        [XmlElement("Fax", typeof(string))]
        public string Fax { get; set; }
    }
}
