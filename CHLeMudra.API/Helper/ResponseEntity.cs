using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHLeMudra.API.Helper
{
    public class ResponseEntity
    {
        public bool Status { get; set; }

        public string Message { get; set; }
        public object Data { get; set; }


    }
}
