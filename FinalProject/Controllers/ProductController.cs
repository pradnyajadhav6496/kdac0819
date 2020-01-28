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

    public class ProductController : BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();

        // GET: api/Product
        public ResponseData Get()
        {
            //T_Products pObj;
            //string catName;
            T_Products obj;
           // obj.T_Category
            List<T_Products> allProducts = dalObject.T_Products.Include("T_Category").ToList();
           // List <T_Category> allCategories = dalObject.T_Category.ToList();

            response.Data = allProducts;
            LoggerFactory.GetLogger(1).Log("Getting Product Details");
            return response;
        }

        // GET: api/Product/5
        public ResponseData Get(int id)
        {
            T_Products product = dalObject.T_Products.Find(id);
            response.Data = product;
            LoggerFactory.GetLogger(1).Log("Getting Specific Product Details");
            return response;
        }

        // POST: api/Product
        public ResponseData Post([FromBody]T_Products product)
        {
            dalObject.T_Products.Add(product);
            dalObject.SaveChanges();
            response.Status="Product Details Added Successfully";
            LoggerFactory.GetLogger(1).Log("Adding New Product");
            return response;
        }

        // PUT: api/Product/5
        public ResponseData Put(int id, [FromBody]T_Products product)
        {
            T_Products productToBeUpdated = dalObject.T_Products.Find(id);
            productToBeUpdated.ProductName = product.ProductName;
            productToBeUpdated.Weight = product.Weight;
            productToBeUpdated.Availability = product.Availability;
            productToBeUpdated.Price = product.Price;
            dalObject.SaveChanges();
            response.Status = "Product Updated Successfully";
            LoggerFactory.GetLogger(1).Log("Updating Product Details");
            return response;
        }

        [HttpPut]
        [Route("api/Product/UpdateAvailability/{id}")]
        public ResponseData UpdateAvailability(int id, [FromBody]T_Products product)
        {
            T_Products productToBeUpdated = dalObject.T_Products.Find(id);
            productToBeUpdated.Availability = product.Availability;
            dalObject.SaveChanges();
            response.Status = "Availability Updated Successfully";
            LoggerFactory.GetLogger(1).Log("Updating Product Details");
            return response;
        }

        [HttpGet]
        [Route("api/Product/ProductsOfSpecCategory/{id}")]
        public ResponseData ProductsOfSpecCategory(int id)
        {
            List<T_Products> products = dalObject.T_Products.ToList();
            response.Data = from prod in products
                            where prod.CategoryId == id && prod.Availability>0
                            select prod;
            return response;
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
}
