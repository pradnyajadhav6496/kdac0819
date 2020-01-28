using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using MyLoggerLib;

namespace FinalProject.Controllers
{
    public class LoginController : BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        // GET: api/Login
        public LoginController()
        {
            dalObject.Configuration.ProxyCreationEnabled = true;

        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public ResponseData Post([FromBody]T_Users u1)
        {
            ResponseData res = new ResponseData();
            if(u1.EmailId!=null && u1.Password!=null)
            {
                var userList = dalObject.T_Users.ToList();
                T_Users validUser = (from user in userList
                                     where user.EmailId == u1.EmailId && user.Password == u1.Password
                                     select user).SingleOrDefault();
                if(validUser!=null)
                {
                    res.Data = validUser;
                    res.Status = "Success";
                    res.Error = null;
                    LoggerFactory.GetLogger(1).Log("Checking user is valid or not");
                    return res;
                }
                else
                {
                    res.Error = null;
                    res.Status = "Incorrect Credentials";
                    LoggerFactory.GetLogger(1).Log("Checking user is valid or not");
                    return res;
                }
            }
            else
            {
                res.Error = null;
                res.Status = "Fields are empty";
                LoggerFactory.GetLogger(1).Log("Checking user is valid or not");
                return res;
            }
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }

       
    }
}
