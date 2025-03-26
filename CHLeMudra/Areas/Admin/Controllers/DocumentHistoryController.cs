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
using CHLeMudra.Entity;
using CHLeMudra.Data;
using System.Linq;
using CHLeMudra.Helper;
using CHLeMudra.Proxy;

namespace CHLeMudra.Areas.Admin.Controllers
{
    public class DocumentHistoryController : Controller
    {
        IDocumentSignHistoryRepository _documentSign;
        IGenericRepository<DocumentAssignee> _docuAssinee;
        public DocumentHistoryController()
        {
            _documentSign = new DocumentSignHistoryRepository();
            _docuAssinee = new GenericRepository<DataBaseContext, DocumentAssignee>();
        }

        public ActionResult Index()
        {
            

            var model = _documentSign.GetAll().Where(c => c.Status.ToLower() == "sent").OrderByDescending(c => c.CreatedOn);
            return View(model);
        }

        public ActionResult DocumentSigned()
        {
            var model = _documentSign.GetAll().Where(c => c.Status.ToLower() == "Completed").OrderByDescending(c => c.CreatedOn);
            return View(model);
        }

        public ActionResult SyncSignedDocument()
        {
            SyncDocument();
            return RedirectToAction("DocumentSigned");
        }

        public void SyncDocument()
        {
            string directorypath = Server.MapPath("~/Documents/" + "ReceivedDoc/");
            if (!Directory.Exists(directorypath))
                Directory.CreateDirectory(directorypath);
            var documentList = _documentSign.GetAll().Where(c => c.Status.ToLower() == "sent").OrderBy(c => c.CreatedOn).ToList();
            string param = "?WorkFlowId=";
            foreach (var item in documentList)
            {
                param = param + item.DocumentId;
                ResponseEntity Response = CommonApi.GetApiURLData(ConfigSettings.eMudraSignApiUrl + APiHelper.DownloadWorkflowDocuments + param, ConfigSettings.Access_Token, true, true);
                DownloadWorkflowDocuments objData = JsonConvert.DeserializeObject<DownloadWorkflowDocuments>(Response.Data.ToString());
                if (objData != null && objData.Response != null && objData.Response.Count > 0)
                {
                    var doc = objData.Response.Where(c => c.IsAttachment == false).FirstOrDefault();
                    if (doc != null)
                    {
                        item.SignDocumentPath = item.DocumentId + "_Signed.pdf";
                        string filePath = directorypath + item.SignDocumentPath;
                        try
                        {
                            if (System.IO.File.Exists(filePath))
                                System.IO.File.Delete(filePath);
                        }
                        catch (Exception)
                        { }
                        byte[] bytes = Convert.FromBase64String(doc.Base64FileData);
                        System.IO.File.WriteAllBytes(filePath, bytes);
                        item.Status = "completed";
                        _documentSign.Update(item);
                    }
                }

            }
        }








    }


}