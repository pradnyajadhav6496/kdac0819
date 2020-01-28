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
    public class AddressController : ApiController
    {
        // GET: api/Address
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/Address/GettingAddressOnUserid/{id}")]
        public ResponseData GettingAddressOnUserid(int id)
        {
            List<T_Address> allAddresses = dalObject.T_Address.ToList();
            response.Data = (from add in allAddresses
                             where add.UserId == id
                             select add).SingleOrDefault();
            LoggerFactory.GetLogger(1).Log("Getting Specific User's Address details");
            return response;
        }

        // POST: api/Address
        public ResponseData Post([FromBody]T_Address address)
        {
            dalObject.T_Address.Add(address);
            dalObject.SaveChanges();
            response.Status = "Address Details Added Successfully";
            LoggerFactory.GetLogger(1).Log("Adding User's Address details");
            return response;
        }

        // PUT: api/Address/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Address/5
        public void Delete(int id)
        {
        }
    }
}
