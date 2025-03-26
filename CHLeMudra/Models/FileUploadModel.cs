using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CHLeMudra.Web.Models
{
    public class FileUploadModel
    {
        [UIHint("FileUploader")]
        public string UploadFile { get; set; }
    }
}