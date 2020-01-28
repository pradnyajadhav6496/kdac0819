using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyLoggerLib;

namespace FinalProject.Filters
{
    public class ProjectFilter:ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string method = actionContext.ActionDescriptor.ActionName;

            LoggerFactory.GetLogger(1).Log(string.Format("/{0}/{1} execution is about to start.", controller, method));

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string controller = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string method = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            LoggerFactory.GetLogger(1).Log(string.Format("/{0}/{1} execution Completed.", controller, method));
            base.OnActionExecuted(actionExecutedContext);
        }
        
    }
}