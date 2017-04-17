using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LandBClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Login",
                "Login",
                new {controller="Home",action="Login"}  
            );
            routes.MapRoute(
                "Logout",
                "Logout",
                new { controller = "Home", action = "Logout" }
            );
            routes.MapRoute(
                "Index",
                "Index",
                new { controller = "Admin", action = "Index" }
                );
            routes.MapRoute(
                "Home",
                "Home",
                new { controller="Home",action="Home"}
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
