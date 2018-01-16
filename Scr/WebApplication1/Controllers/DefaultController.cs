using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using ClassLibrary1;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            ViewBag.Something = "Whatever";
            MyFirstModel model = new MyFirstModel()
            {
                MyFirstValue = "Model value 1",
                MySecondValue = "Model value 2",
                MyList = new List<string>
                {
                   "hej","hello"
                }
            };
            return View(model);
        }
        public ActionResult Sida2()
        {
            Class1 myClass = new Class1();
            int result = myClass.AddNumbers(2,2);
            return View();
        }
    }
}