using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

//viktig fil som ej får tas bort om något ska fungera. Det första som körs när man anropar webbservern. Finns lite olika events, application_start är 
//ett sådant event. Man kan även lägga in egna mer specifika events.
namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
