using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FinalProject.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;
using MyLoggerLib;

namespace FinalProject.Controllers
{

    public class UploadApiController : BaseController
    {

        public async Task<HttpResponseMessage> PostFormData()
        {

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Photos");
            var provider = new CustomMultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                ResponseData responseStatus = new ResponseData() { Status = "Success", Error = null };
                LoggerFactory.GetLogger(1).Log("Uploading photo");
                return Request.CreateResponse(HttpStatusCode.OK, responseStatus);
            }
            catch (System.Exception e)
            {
                LoggerFactory.GetLogger(1).Log("Caught exception while uploading photo");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }


    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }
}
