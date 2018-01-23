using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        //route colletion som manipulerar. Ger ett namn och en url. Standardroutern är nedan angivna vid "url:"
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Root",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "GameSession.PlaceMark",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "GameSession", action = "PlaceMark", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "GameSession.ShowGameBoard",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "GameSession", action = "ShowGameBoard", id = UrlParameter.Optional }
           );
        }
    }
}
