using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using MyLoggerLib;
using FinalProject.Filters;

namespace FinalProject.Controllers
{
    
    public class PasswordHistoryLogController : ApiController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();
        // GET: api/PasswordHistoryLog
        public ResponseData Get()
        {
            response.Data= dalObject.T_PasswordHistoryLog.ToList();
            LoggerFactory.GetLogger(1).Log("Getting Password History Log of users");
            return response;
        }

        // GET: api/PasswordHistoryLog/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PasswordHistoryLog
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PasswordHistoryLog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PasswordHistoryLog/5
        public void Delete(int id)
        {
        }
    }
}
