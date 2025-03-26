using CHLeMudra.Api.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CHLeMudra.API.Proxy
{
    [XmlRoot(ElementName = "BulkData")]
    public class BulkData
    {

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "address")]
        public string Address { get; set; }

        [XmlElement(ElementName = "country")]
        public string Country { get; set; }

        [XmlElement(ElementName = "phone")]
        public string Phone { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "vdesignation")]
        public string Vdesignation { get; set; }

        [XmlElement(ElementName = "vdate")]
        public DateTime Vdate { get; set; }

        [XmlElement(ElementName = "cname")]
        public string Cname { get; set; }

        [XmlElement(ElementName = "cdesignation")]
        public string Cdesignation { get; set; }

        [XmlElement(ElementName = "cdate")]
        public DateTime Cdate { get; set; }

        [XmlElement(ElementName = "citystate")]
        public string Citystate { get; set; }

        [XmlElement(ElementName = "zipcode")]
        public string Zipcode { get; set; }

        [XmlElement(ElementName = "nationality")]
        public string Nationality { get; set; }

        [XmlElement(ElementName = "pan")]
        public string Pan { get; set; }

        [XmlElement(ElementName = "beneficiary")]
        public string Beneficiary { get; set; }

        [XmlElement(ElementName = "bank")]
        public string Bank { get; set; }

        [XmlElement(ElementName = "bankaddress")]
        public string Bankaddress { get; set; }

        [XmlElement(ElementName = "bankaccount")]
        public string Bankaccount { get; set; }

        [XmlElement(ElementName = "accounttype")]
        public string Accounttype { get; set; }

        [XmlElement(ElementName = "ifsc")]
        public string Ifsc { get; set; }

        [XmlElement(ElementName = "paypalemail")]
        public string Paypalemail { get; set; }
    }

    [XmlRoot(ElementName = "DocumentElement")]
    public class DocumentElement
    {

        [XmlElement(ElementName = "BulkData")]
        public BulkData BulkData { get; set; }
    }

}
