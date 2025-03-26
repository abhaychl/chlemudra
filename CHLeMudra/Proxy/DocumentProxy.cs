using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHLeMudra.Proxy
{
    public class DocumentProxy
    {

        public List<DocumentRequestProxy> DocumentRequestProxy { get; set; }

    }
    public class DocumentRequestProxy
    {
        public bool DonotSendCompletionMailToParticipants { get; set; }
        public int TemplateId { get; set; }
        public string DocumentName { get; set; }
        public string FileData { get; set; }
        public int SigningType { get; set; }
        public List<Signatory> Signatories { get; set; }
        public string ReferenceNo { get; set; }
        public List<ListAttachmentDetail> listAttachmentDetails { get; set; }
        public string WaterMark { get; set; }
        public string DocumentProtection { get; set; }
    }
    public class Signatory
    {
        public int SignerType { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public string Page { get; set; }
        public string EmailId { get; set; }
    //    public int ModeOfSignature { get; set; }
        public int MailSendingOptions { get; set; }
    }
    public class Coordinate
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PageNumber { get; set; }
        public string SelectedPage { get; set; }
    }
    public class ListAttachmentDetail
    {
        public string DocumentName { get; set; }
        public string Base64fileData { get; set; }
        public string Description { get; set; }
        public string DocumentPassword { get; set; }
        public int WorkflowId { get; set; }
    }
}