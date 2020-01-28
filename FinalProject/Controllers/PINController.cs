using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyLoggerLib;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class PINController : BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();
        // GET: api/PIN
        public ResponseData Get()
        {
            List<T_PIN> allPin = dalObject.T_PIN.ToList();
            response.Data = allPin;
            LoggerFactory.GetLogger(1).Log("Getting PIN details");
            return response;
        }

        // GET: api/PIN/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PIN
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PIN/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PIN/5
        public void Delete(int id)
        {
        }
    }
}
