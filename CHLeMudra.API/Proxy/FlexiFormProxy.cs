using CHLeMudra.Api.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHLeMudra.API.Proxy
{
    public class FlexiFormProxy {
        public int TemplateId { get; set; }
        public string AppName { get; set; }
        public string XmlData { get; set; }
        public List<Signatoryflex> Signatory{get;set;}
    }

  
}
