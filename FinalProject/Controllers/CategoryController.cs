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
    public class CategoryController : BaseController
    {
        FinalProjectDbEntities dalObject = new FinalProjectDbEntities();
        ResponseData response = new ResponseData();
        // GET: api/Category
        public ResponseData Get()
        {
            List<T_Category> allCategories= dalObject.T_Category.ToList();
            response.Data = allCategories;
            LoggerFactory.GetLogger(1).Log("Getting Category Details");
            return response;
        }

        // GET: api/Category/5
        public ResponseData Get(int id)
        {
            T_Category cat = dalObject.T_Category.Find(id);
            response.Data = cat;
            LoggerFactory.GetLogger(1).Log("Getting Specific Category Details");
            return response;
        }

        // POST: api/Category
        public ResponseData Post([FromBody]T_Category cat)
        {
            dalObject.T_Category.Add(cat);
             dalObject.SaveChanges();
            response.Status = "Category Added Successfully";
            LoggerFactory.GetLogger(1).Log("Adding New Category");

            return response;
        }

        // PUT: api/Category/5
        public ResponseData Put(int id, [FromBody]T_Category categoryToBeUpdated)
        {
            T_Category cat = dalObject.T_Category.Find(id);
            cat.Category = categoryToBeUpdated.Category;
            dalObject.SaveChanges();
            response.Status="Category Updated Successfully";
            LoggerFactory.GetLogger(1).Log("Updating Category Details");
            return response;
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }
    }
}
