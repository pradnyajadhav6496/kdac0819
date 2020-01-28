using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ResponseData//responseData  
    {
        public string Status { get; set; }
        public Exception Error { get; set; }
        public dynamic Data { get; set; }  //no object because boxing and unboxing takes time
    }
    //response data when send back internally sends http response message
}