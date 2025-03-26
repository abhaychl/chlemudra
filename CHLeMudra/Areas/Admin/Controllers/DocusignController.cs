using CHLeMudra.Models;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Document = DocuSign.eSign.Model.Document;
using CHLeMudra.Entity;
using CHLeMudra.Data;
using CHLeMudra.Proxy;
using CHLeMudra.Helper;
using log4net;
using System.Linq;

namespace CHLeMudra.Areas.Admin.Controllers
{
    public class DocusignController : Controller
    {

        IDocumentSignHistoryRepository _documentSign;
        IGenericRepository<DocumentAssignee> _docuAssinee;
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        public DocusignController()
        {
            _documentSign = new DocumentSignHistoryRepository();
            _docuAssinee = new GenericRepository<DataBaseContext, DocumentAssignee>();
        }


        public ActionResult SendDocumentforSign()
        {
            // GenerateToken();
            return View();
        }

        [HttpPost]
        public ActionResult SendDocumentforSign(DocumentModel model, HttpPostedFileBase UploadDocument)
        {
            Recipient recipientModel = new Recipient();
            byte[] data;
            using (Stream inputStream = UploadDocument.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            try
            {

                //stroring file in directory
                //string directorypath = System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/" + "Files/");
                //if (!Directory.Exists(directorypath))
                //{
                //    Directory.CreateDirectory(directorypath);

                //}
                //string fileName = $@"{DateTime.Now.Ticks}.pdf";
                //var path = directorypath + fileName;
                //System.IO.File.WriteAllBytes(path, model.Files);

                //Creating Root Document request
                DocumentRequestProxy documentRequestProxy = new DocumentRequestProxy();
                documentRequestProxy.DonotSendCompletionMailToParticipants = false;
                documentRequestProxy.TemplateId = Convert.ToInt32(ConfigSettings.TemplateId);
                documentRequestProxy.DocumentName = model.DocumentTitle;
                //documentRequestProxy.FileData = "";
                documentRequestProxy.FileData = System.Convert.ToBase64String(data);
                documentRequestProxy.SigningType = 2;//1 for Serial, 2 for Parallel(works only for Adhoc Signing)
                documentRequestProxy.DocumentProtection = "";
                documentRequestProxy.ReferenceNo = "";
                documentRequestProxy.WaterMark = "";
                documentRequestProxy.Signatories = new List<Signatory>();
                //Adding Signatory
                if (model.Recipients != null && model.Recipients.Count > 0)
                {
                    Signatory signatory;
                    Coordinate coordinate;
                    foreach (Recipient sing in model.Recipients)
                    {
                        if (!string.IsNullOrEmpty(sing.Email) && !string.IsNullOrEmpty(sing.Name))
                        {
                            signatory = new Signatory();
                            signatory.SignerType = 1;// 1 for Signer and 2 for Reviewer (works only for Adhoc Signing) 
                            signatory.EmailId = sing.Email;
                            signatory.Page = "";
                            //  signatory.ModeOfSignature = 0;
                            signatory.MailSendingOptions = 0;
                            //adding coordinate
                            signatory.Coordinates = new List<Coordinate>();
                            coordinate = new Coordinate();
                            coordinate.SelectedPage = "";
                            signatory.Coordinates.Add(coordinate);
                            documentRequestProxy.Signatories.Add(signatory);
                        }
                    }

                }

                //Adding List of Attachement
                documentRequestProxy.listAttachmentDetails = new List<ListAttachmentDetail>();
                ListAttachmentDetail listAttachmentDetail = new ListAttachmentDetail();
                listAttachmentDetail.DocumentName = model.DocumentTitle;
                listAttachmentDetail.Base64fileData = System.Convert.ToBase64String(data);
                listAttachmentDetail.Description = "";
                listAttachmentDetail.DocumentPassword = "";
                documentRequestProxy.listAttachmentDetails.Add(listAttachmentDetail);
                List<DocumentRequestProxy> DocumentProxy = new List<DocumentRequestProxy>();
                DocumentProxy.Add(documentRequestProxy);
                // var token = GenerateToken();
                //if (token != null && token.Response != null)
                //{
                //    ResponseEntity Response = CommonApi.PostApiURLData(ConfigSettings.eMudraSignApiUrl + APiHelper.InitiateAndSign, DocumentProxy, token.Response.AuthToken, false, true);
                //    var objData = JsonConvert.DeserializeObject<ValidateLoginApiModel>(Response.Data.ToString());

                //}
                var JsonFormat = JsonConvert.SerializeObject(DocumentProxy);
                ResponseEntity Response = CommonApi.PostApiURLData(ConfigSettings.eMudraSignApiUrl + APiHelper.InitiateAndSign, DocumentProxy, ConfigSettings.Access_Token, false, true);
                InitSignResponse objData = JsonConvert.DeserializeObject<InitSignResponse>(Response.Data.ToString());
                if (objData != null && objData.IsSuccess && objData.Response != null)
                {
                    DocumentSignHistory documentSign = new DocumentSignHistory();
                    if (objData.Response.DocumentNumberList != null && objData.Response.DocumentNumberList.Length > 0)
                    {
                        documentSign.DocumentNo = objData.Response.DocumentNumberList[0];
                        documentSign.DocumentId = documentSign.DocumentNo.Split('/').Last();
                    }
                    documentSign.WorkflowId = objData.Response.WorkflowId;
                    documentSign.EMudraStatus = objData.Response.Status;
                    documentSign.ReferenceNo = objData.Response.ReferenceNo;
                    documentSign.Status = "Sent";
                    documentSign.CreatedOn = System.DateTime.Now;
                    if (_documentSign.Save(documentSign))
                    {
                        DocumentAssignee Assignee;
                        foreach (Recipient sing in model.Recipients)
                        {
                            Assignee = new DocumentAssignee();
                            Assignee.DocumentSignHistoryId = documentSign.Id;
                            Assignee.Email = sing.Email;
                            Assignee.Name = sing.Name;
                            Assignee.CreatedOn = System.DateTime.Now;
                            _docuAssinee.Save(Assignee);
                        }
                    }

                    ViewBag.Message = "Document successfuly sent.";
                }
                else
                {

                    ViewBag.Message = "Document sending process failed.";
                }

            }
            catch (Exception ex)
            {
                Logger.Error("DocuSign Lead.GenerateToken()", ex);
            }
            return View();
        }


        public ActionResult SendDocumentforFlexySign()
        {
            // GenerateToken();
            return View();
        }

        [HttpPost]
        public ActionResult SendDocumentforFlexySign(DocumentModel model)
        {
            List<FlexiSignature> flexyproxy = new List<FlexiSignature>();
            FlexiSignature flexiModel = new FlexiSignature();
            flexiModel.Data = "<DocumentElement><BulkData><name>Om Saini</name><address>Saharanpur</address></BulkData></DocumentElement>";
            flexiModel.TemplateId = Convert.ToInt32(ConfigSettings.TemplateId); 
            flexiModel.DonotSendCompletionMailToParticipants = true;
            flexiModel.WaterMark = "";
            flexiModel.DocumentProtection = "";
            flexiModel.SigningType = 0;
            if (model.Recipients != null && model.Recipients.Count > 0)
            {
                Signatoryflex signatory;
                foreach (Recipient sing in model.Recipients)
                {
                    if (!string.IsNullOrEmpty(sing.Email) && !string.IsNullOrEmpty(sing.Name))
                    {
                        signatory = new Signatoryflex();
                        signatory.EmailId = sing.Email;
                        signatory.ModeOfSignature = "";
                        signatory.ReviewerComment = "";
                        flexiModel.Signatories.Add(signatory);
                    }
                }

            }

            ListAttachmentDetail listAttachmentDetail = new ListAttachmentDetail();
            listAttachmentDetail.DocumentName = "";
            listAttachmentDetail.Base64fileData ="";
            listAttachmentDetail.Description = "";
            listAttachmentDetail.DocumentPassword = "";
            flexiModel.listAttachmentDetails.Add(listAttachmentDetail);
            try
            {
                flexyproxy.Add(flexiModel);
               var JsonFormat = JsonConvert.SerializeObject(flexiModel);
                ResponseEntity Response = CommonApi.PostApiURLData(ConfigSettings.eMudraSignApiUrl + APiHelper.InitiateAndSignFlexiForm, flexyproxy, ConfigSettings.Access_Token, false, true);
                InitSignResponse objData = JsonConvert.DeserializeObject<InitSignResponse>(Response.Data.ToString());
                if (objData != null && objData.IsSuccess && objData.Response != null)
                {
                    DocumentSignHistory documentSign = new DocumentSignHistory();
                    if (objData.Response.DocumentNumberList != null && objData.Response.DocumentNumberList.Length > 0)
                    {
                        documentSign.DocumentNo = objData.Response.DocumentNumberList[0];
                        documentSign.DocumentId = documentSign.DocumentNo.Split('/').Last();
                    }
                    documentSign.WorkflowId = objData.Response.WorkflowId;
                    documentSign.EMudraStatus = objData.Response.Status;
                    documentSign.ReferenceNo = objData.Response.ReferenceNo;
                    documentSign.Status = "Sent";
                    documentSign.CreatedOn = System.DateTime.Now;
                    if (_documentSign.Save(documentSign))
                    {
                        DocumentAssignee Assignee;
                        foreach (Recipient sing in model.Recipients)
                        {
                            Assignee = new DocumentAssignee();
                            Assignee.DocumentSignHistoryId = documentSign.Id;
                            Assignee.Email = sing.Email;
                            Assignee.Name = sing.Name;
                            Assignee.CreatedOn = System.DateTime.Now;
                            _docuAssinee.Save(Assignee);
                        }
                    }

                    ViewBag.Message = "Document successfuly sent.";
                }
                else
                {

                    ViewBag.Message = "Document sending process failed.";
                }

            }
            catch (Exception ex)
            {
                Logger.Error("DocuSign Lead.GenerateToken()", ex);
            }
            return View();
        }

        private ValidateLoginApiModel GenerateToken()
        {
            try
            {
                var JsonData = new Dictionary<string, string>
                {
                       { "UserName", ConfigSettings.UserName },
                       { "Password", ConfigSettings.Password },
                 };
                ResponseEntity Response = CommonApi.PostApiURLData(ConfigSettings.eMudraSignApiUrl + APiHelper.ValidateLogin, JsonData, ConfigSettings.SecretKey, false, true);
                var objData = JsonConvert.DeserializeObject<ValidateLoginApiModel>(Response.Data.ToString());
                return objData;

            }
            catch (System.Exception ex)
            {
                Logger.Error("DocuSign Lead.GenerateToken()", ex);
                return null;
            }
        }

    }





}