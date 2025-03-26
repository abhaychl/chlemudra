using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHLeMudra.Models
{
    public class DocumentModel
    {
        public byte[] Files { get; set; }
        public string DocumentTitle { get; set; }
        //public string Email { get; set; }
        //public Recipient Recipient { get; set; }
        public List<Recipient> Recipients { get; set; }
    }

    public  class Recipient
    {
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Description { get; set; }
    }
}