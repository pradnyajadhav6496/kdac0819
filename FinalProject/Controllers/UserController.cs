using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using System.Net.Mail;
using FinalProject.Filters;
using MyLoggerLib;

namespace FinalProject.Controllers
{
    public class UserController :BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();
        UserController()
        {
           // dalObject.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/User
        public ResponseData Get()
        {
            List<T_Users> allUsers= dalObject.T_Users.ToList();

            response.Data = from u in allUsers
                            where u.RoleId == 2
                            select u;
            response.Status = "success";
            LoggerFactory.GetLogger(1).Log("Getting User Details");
            return response;           
        }

        // GET: api/User/5
        public string Get(int id)
        {
            LoggerFactory.GetLogger(1).Log("Getting specific user details");
            return "value";
        }

        // POST: api/User
        public ResponseData Post([FromBody]T_Users user)
        {
            dalObject.T_Users.Add(user);
            dalObject.SaveChanges();
            response.Status = "Successfully Registered";
            LoggerFactory.GetLogger(1).Log("Registring new user");
            return response;
        }

        // PUT: api/User/5
        public ResponseData Put(int id, [FromBody]T_Users user)
        {
            T_Users userToBeUpdated = dalObject.T_Users.Find(id);
            userToBeUpdated.EmailId = user.EmailId;
            userToBeUpdated.Name = user.Name;
            userToBeUpdated.Password = user.Password;
            userToBeUpdated.MobileNo = user.MobileNo;
            dalObject.SaveChanges();
            response.Status = "Successfully updated";
            LoggerFactory.GetLogger(1).Log("Updating user details");
            return response;
        }

        // DELETE: api/User/5
        public ResponseData Delete(int id)
        {
            T_Users userToBeDeleted = dalObject.T_Users.Find(id);
            dalObject.T_Users.Remove(userToBeDeleted);
            dalObject.SaveChanges();
            response.Status = "successfully deleted";
            LoggerFactory.GetLogger(1).Log("Deleting user details");
            return response;
        }

        [HttpPost]
        [Route("api/User/SendMail")]
        public ResponseData SendEmail(Email e)
        {
            string to = e.To;
            string subject = e.Subject;
            string body = e.Body;
            ResponseData response = new ResponseData();

            string result = "Message Sent Successfully..!!";
            string senderID = "pcjadhav6767@gmail.com";// use sender’s email id here..
            const string senderPassword = "P8806326053"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, to, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
                response.Error = ex;
            }
            LoggerFactory.GetLogger(1).Log("Sending Email");
            return response;

        }

    }

}

