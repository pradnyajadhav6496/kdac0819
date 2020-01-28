using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using System.Web.Http.Cors;
using MyLoggerLib;

namespace FinalProject.Controllers
{
    // Allow CORS for all origins. (Caution!)
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();
        // GET: api/Role
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public ResponseData Get()
        {
            List<T_Roles> allRoles = dalObject.T_Roles.ToList();
            response.Data = allRoles;
            LoggerFactory.GetLogger(1).Log("Getting role details");
            return response;
        }

        // GET: api/Role/5
        public ResponseData Get(int id)
        {
            T_Roles role = dalObject.T_Roles.Find(id);
            response.Data = role;
            LoggerFactory.GetLogger(1).Log("Getting specific role details");
            return response;
        }

        // POST: api/Role
        public ResponseData Post([FromBody]T_Roles role)
        {
            dalObject.T_Roles.Add(role);
            dalObject.SaveChanges();
            response.Status = "Successfully Inserted";
            LoggerFactory.GetLogger(1).Log("Adding new role");
            return response;
        }

        // PUT: api/Role/5
        public ResponseData Put(int id, [FromBody]T_Roles role)
        {
            T_Roles roleToUpdated = dalObject.T_Roles.Find(id);
            roleToUpdated.RoleName = role.RoleName;
            dalObject.SaveChanges();
            response.Status = "Successfully Updated";
            LoggerFactory.GetLogger(1).Log("Updating role details");
            return response;
        }

        // DELETE: api/Role/5
        public ResponseData Delete(int id)
        {
            T_Roles role = dalObject.T_Roles.Find(id);
            dalObject.T_Roles.Remove(role);
            dalObject.SaveChanges();
            response.Status = "Successfully Deleted";
            LoggerFactory.GetLogger(1).Log("Deleting role details");
            return response;
        }
    }
}
