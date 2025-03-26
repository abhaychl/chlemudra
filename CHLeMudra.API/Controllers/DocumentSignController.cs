
using CHLeMudra.Api.Entity;
using CHLeMudra.Api.Proxy;
using CHLeMudra.Api.Repository;
using CHLeMudra.API.Helper;
using CHLeMudra.API.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CHLeMudra.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentSignController : ControllerBase
    {
        private readonly IOptions<ServiceSettings> _serviceSettings;
        private readonly IApiHelper _apiHelper;
        private readonly IDocumentSignHistoryRepository _documentSign;
        private readonly IDocumentAssigneeRepository _documentAssignee;
        public DocumentSignController(IOptions<ServiceSettings> serviceSettings, IApiHelper apiHelper, IDocumentSignHistoryRepository documentSign, IDocumentAssigneeRepository documentAssignee)
        {
            _serviceSettings = serviceSettings;
            _apiHelper = apiHelper;
            _documentSign = documentSign;
            _documentAssignee = documentAssignee;
        }

        [HttpGet]
        [Route("GetAccessToken")]
        public async Task<string> GenerateAccessToken()
        {
           return _apiHelper.GenerateAccessToken();
        }

        [HttpGet]
        [Route("GetWorkflowInfo")]
        public async Task<ResponseEntity> GetWorkflowInfo(int WorkflowId)
        {
            ResponseEntity Response = _apiHelper.GetApiURLData(_serviceSettings.Value.EMudraSignApiUrl + ApiMethod.GetWorkflowInfo + "?WorkFlowId=" + WorkflowId);

            //  DownloadWorkflowDocuments objData = JsonConvert.DeserializeObject<DownloadWorkflowDocuments>(Response.Data.ToString());

            return Response;

        }
        [HttpGet]
        [Route("GetSignDocument")]
        public async Task<ResponseEntity> GetSignDocument(int WorkflowId)
        {
            ResponseEntity Response = _apiHelper.GetApiURLData(_serviceSettings.Value.EMudraSignApiUrl + ApiMethod.DownloadWorkflowDocuments + "?WorkFlowId=" + WorkflowId);

            //  DownloadWorkflowDocuments objData = JsonConvert.DeserializeObject<DownloadWorkflowDocuments>(Response.Data.ToString());

            return Response;
        }
        [HttpPost]
        [Route("SendFlexiSign")]
        public async Task<ResponseEntity> SignFlexiForm(FlexiFormProxy objProxy)
        {
            List<FlexiSignature> flexyPayload = new List<FlexiSignature>();
            FlexiSignature flexiModel = new FlexiSignature();
            //flexiModel.Data = "<DocumentElement><BulkData><name>Om Saini</name><address>Saharanpur</address></BulkData></DocumentElement>";
            flexiModel.Data = objProxy.XmlData;
            flexiModel.TemplateId = objProxy.TemplateId;
            flexiModel.DonotSendCompletionMailToParticipants = true;
            flexiModel.WaterMark = "";
            flexiModel.DocumentProtection = "";
            flexiModel.SigningType = 0;
            if (objProxy.Signatory != null && objProxy.Signatory.Count > 0)
                flexiModel.Signatories.AddRange(objProxy.Signatory);

            ListAttachmentDetail listAttachmentDetail = new ListAttachmentDetail();
            listAttachmentDetail.DocumentName = "";
            listAttachmentDetail.Base64fileData = "";
            listAttachmentDetail.Description = "";
            listAttachmentDetail.DocumentPassword = "";
            flexiModel.listAttachmentDetails.Add(listAttachmentDetail);
            try
            {
                flexyPayload.Add(flexiModel);
                var JsonFormat = JsonConvert.SerializeObject(flexiModel);
                ResponseEntity Response = _apiHelper.PostApiURLData(_serviceSettings.Value.EMudraSignApiUrl + ApiMethod.InitiateAndSignFlexiForm, flexyPayload);
                if (Response.Status)
                {
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
                        documentSign.DocumentType = "Flexi";
                        documentSign.TemplateId = objProxy.TemplateId;
                        documentSign.AppName = objProxy.AppName;
                        documentSign.Status = "Sent";
                        documentSign.CreatedOn = System.DateTime.Now;
                        bool status = await _documentSign.AddAsyn(documentSign);
                        if (status)
                        {
                            DocumentAssignee Assignee;
                            foreach (var sing in objProxy.Signatory)
                            {
                                Assignee = new DocumentAssignee();
                                Assignee.DocumentSignHistoryId = documentSign.Id;
                                Assignee.Email = sing.EmailId;
                                //Assignee.Name = sing.Name;
                                Assignee.CreatedOn = System.DateTime.Now;
                                await _documentAssignee.AddAsyn(Assignee);
                            }
                        }
                        return new ResponseEntity
                        {
                            Status = true,
                            Message = "Document successfuly sent.",
                            Data = documentSign
                        };
                    }
                    else
                    {
                        return new ResponseEntity
                        {
                            Status = false,
                            Message = "Document sending process failed.",

                        };
                    }
                }
                else
                {
                    return Response;
                }

            }
            catch (Exception ex)
            {
                return new ResponseEntity
                {
                    Status = true,
                    Message = ex.Message,

                };
                //Logger.Error("DocuSign Lead.GenerateToken()", ex);
            }

        }

    }
}
