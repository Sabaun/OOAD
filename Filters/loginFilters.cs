using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Filters
{

    public class loginFilters : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["IsLogedIn"] == null)
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {

                    {"Area", ""},
                    {"Controller", "Home"},
                    {"Action", "Index"}
                });

            }
            base.OnActionExecuting(filterContext);
        }
    }

}