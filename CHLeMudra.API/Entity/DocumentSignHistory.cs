using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHLeMudra.Api.Entity
{
   public class DocumentSignHistory
    {
        public int Id { get; set; }
        public string DocumentId { get; set; }
        public string DocumentNo { get; set; }
        public int WorkflowId { get; set; }
        public int TemplateId { get; set; }
        public string DocumentType { get; set; }
        public string AppName { get; set; }
        public bool EMudraStatus { get; set; }
        public string ReferenceNo { get; set; }
        public string SignDocumentPath { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }        

    }

    public class DocumentAssignee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DocumentSignHistoryId { get; set; }
        public virtual DocumentSignHistory DocumentSignHistory { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
