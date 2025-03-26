using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHLeMudra.Proxy
{
    public class InitSignResponse
    {
        public bool IsSuccess { get; set; }
        public string[] Messages { get; set; }
        public int ErrorCode { get; set; }
        public InitSignDocumentResponse Response { get; set; }
    }

    public class InitSignDocumentResponse
    {
        public string ReferenceNo { get; set; }
        public string[] DocumentNumberList { get; set; }
        public bool Status { get; set; }
        public int WorkflowId { get; set; }
        public string DocumentIdList { get; set; }
    }
}