using CHLeMudra.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHLeMudra.Proxy
{
    public class Signatoryflex
    {
        public string EmailId { get; set; }
        public string ModeOfSignature { get; set; }
        public int MailSendingOptions { get; set; }
        public int ModeofAuthentication { get; set; }
        public string ReviewerComment { get; set; }
    }

    public class FlexiSignature
    {
        public FlexiSignature()
        {
            Signatories = new List<Signatoryflex>();
            listAttachmentDetails = new List<ListAttachmentDetail>();
        }
        public string Data { get; set; }
        public List<Signatoryflex> Signatories { get; set; }
        public int TemplateId { get; set; }
        public bool DonotSendCompletionMailToParticipants { get; set; }
        public List<ListAttachmentDetail> listAttachmentDetails { get; set; }
        public string WaterMark { get; set; }
        public string DocumentProtection { get; set; }
        public int SigningType { get; set; }
    }


    //public class ListAttachmentDetail
    //{
    //    public string DocumentName { get; set; }
    //    public string Base64fileData { get; set; }
    //    public string Description { get; set; }
    //    public string DocumentPassword { get; set; }
    //    public int WorkflowId { get; set; }
    //}


}