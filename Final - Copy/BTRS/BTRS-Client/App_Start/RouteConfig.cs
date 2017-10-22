using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BTRS_Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //     name: "Default-1",
            //     url: "{controller}/{action}/{id}",
            //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            // );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: "http://localhost:3118/Index.html"
           );
            

            
        }
    }
}
