using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicTacToe
{
    public class RouteConfig
    {
        //route colletion som manipulerar. Ger ett namn och en url. Standardroutern är nedan angivna vid "url:"
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapMvcAttributeRoutes();


            routes.MapRoute(
               name: "GameSession",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "GameSession", action = "FirstPage", id = UrlParameter.Optional }
            );
        }
    }
}
