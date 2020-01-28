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
    public class ShoppingCartController : BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();

        // GET: api/ShoppingCart
        public ResponseData Get()
        {

            return response;
        }

        // GET: api/ShoppingCart/5
        public ResponseData Get(int id)
        {
            return response;
        }
        [HttpGet]
        [Route("api/ShoppingCart/GetCartDetails/{id}")]
        public ResponseData GetCartDetails(int id)
        {
            List<T_ShoppingCart> cartDetails = dalObject.T_ShoppingCart.ToList();
            response.Data = from cart in cartDetails
                            where cart.UserId == id && cart.IsDelivered==false
                            select cart;
            LoggerFactory.GetLogger(1).Log("Getting Cart Details of Specific User");
            return response;
        }

        // POST: api/ShoppingCart
        public ResponseData Post([FromBody]T_ShoppingCart cart)
        {
            dalObject.T_ShoppingCart.Add(cart);
            dalObject.SaveChanges();
            response.Status = "success";
            LoggerFactory.GetLogger(1).Log("Adding Cart Details");
            return response;
        }

        [HttpPut]
        [Route("api/ShoppingCart/UpdatingCartStatus/{id}")]
        public ResponseData UpdatingCartStatus(int id, [FromBody]T_ShoppingCart cart)
        {
            T_ShoppingCart cartToUpdated = dalObject.T_ShoppingCart.Find(id);
            cartToUpdated.IsDelivered = cart.IsDelivered;
            dalObject.SaveChanges();
            return response;
        }
        // PUT: api/ShoppingCart/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/ShoppingCart/5
        public ResponseData Delete(int id)
        {
            T_ShoppingCart cart = dalObject.T_ShoppingCart.Find(id);
            dalObject.T_ShoppingCart.Remove(cart);
            dalObject.SaveChanges();
            response.Status = "success";
            LoggerFactory.GetLogger(1).Log("Removing Cart Details");
            return response;
        }
    }
}
