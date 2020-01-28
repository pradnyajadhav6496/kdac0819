using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
using System.Net.Mail;
using MyLoggerLib;

namespace FinalProject.Controllers
{
    public class AccountController : BaseController
    {
        FinalProjectDbEntities dalobj = new FinalProjectDbEntities();
        // GET: api/Account
        public IEnumerable<T_Users> Get()
        {
            return dalobj.T_Users.ToList();
        }

        // GET: api/Account/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost]
        [Route("api/Account/Login")]
        public ResponseData Post([FromBody]T_Users value)
        {
            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId && u.Password == value.Password
                        select u).SingleOrDefault();
            ResponseData responce = new ResponseData();
            if (User != null)
            {
                responce.Data = User;
                responce.Error = null;
                responce.Status = "Success";
                LoggerFactory.GetLogger(1).Log("Checking Login Details:Correct");
                return responce;
            }
            else
            {
                responce.Error = null;
                responce.Status = "Failded";
                LoggerFactory.GetLogger(1).Log("Checking Login Details:Incorrect");
                return responce;
            }

        }



        [HttpPost]
        public ResponseData Register([FromBody]T_Users value)
        {

            ResponseData responce = new ResponseData();
            value.RoleId = 2;//by default User/Customer 
            var result = dalobj.T_Users.Add(value);
            dalobj.SaveChanges();
            if (result != null)
            {
                responce.Data = result;
                responce.Error = null;
                responce.Status = "Success";
                LoggerFactory.GetLogger(1).Log("Registering New User:Success");
                return responce;
            }
            else
            {
                responce.Data = null;
                responce.Error = null;
                responce.Status = "Failed";
                LoggerFactory.GetLogger(1).Log("Registering New User:Failed");
                return responce;
            }
        }



        [HttpPost]
        [Route("api/Account/OTP")]
        public ResponseData OTP([FromBody]dynamic otpDetails)
        {
            string email = otpDetails.EmailId.ToString();


            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = dalobj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null)
            {
                ResponseData RC = new ResponseData();
                RC.Status = "success";
                RC.Error = null;
                RC.Data = vOTP;
                LoggerFactory.GetLogger(1).Log("Checking OTP:Success");
                return RC;
            }
            else
            {
                ResponseData RC = new ResponseData();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                LoggerFactory.GetLogger(1).Log("Checking OTP:Failed");
                return RC;
            }
        }




        [HttpPost]
        [Route("api/Account/IsExist")]
        public ResponseData IsExist([FromBody]T_Users value)
        {
            Email e = new Email();
            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                ResponseData RC = new ResponseData();
                string mails = GetOTP();

                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = DateTime.Now.AddMinutes(5);
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                dalobj.T_OTP_Details.Add(otp);
                dalobj.SaveChanges();
                e.To = User.EmailId;
                e.Subject = "OTP";
                e.Body = "Your OTP to change Password is " + mails + " and is valid Till " + otp.ValidTill + ".";
                SendEmail(e);
                RC.Status = "success";
                RC.Error = null;
                RC.Data = mails;
                LoggerFactory.GetLogger(1).Log("Is User Exist:Success");
                return RC;
            }
            else
            {
                ResponseData RC = new ResponseData();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                LoggerFactory.GetLogger(1).Log("Is User Exist:Failed");
                return RC;
            }

        }


        [HttpPut]
        [Route("api/Account/UpdatePassword")]
        public ResponseData updatepassword([FromBody]T_Users value)
        {

            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                User.Password = value.Password;
                dalobj.SaveChanges();
                ResponseData rc = new ResponseData();
                rc.Status = "success";
                rc.Error = null;
                rc.Data = User;
                LoggerFactory.GetLogger(1).Log("Updating Password:Success");
                return rc;
            }
            else
            {
                ResponseData rc = new ResponseData();
                rc.Status = "fail";
                rc.Error = null;
                rc.Data = null;
                LoggerFactory.GetLogger(1).Log("Updating Password:Failed");
                return rc;
            }
        }
        private string GetOTP(string otpType = "1", int len = 5)
        {
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            LoggerFactory.GetLogger(1).Log("Generaing OTP");
            return otp;
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
            LoggerFactory.GetLogger(1).Log("Sending Mail");
            return response;

        }


    }
}
