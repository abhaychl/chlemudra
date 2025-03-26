using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHLeMudra.Proxy
{
    public class DownloadWorkflowDocuments
    {

        public bool IsSuccess { get; set; }
        public string[] Messages { get; set; }
        public int ErrorCode { get; set; }

        public List<WorkflowDocuments> Response { get; set; }
    }
    public class WorkflowDocuments
    {

        public string DocumentName { get; set; }
        public string Base64FileData { get; set; }
        public int DocumentId { get; set; }
        public bool IsAttachment { get; set; }
    }
}