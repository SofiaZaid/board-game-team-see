using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using GameEngine;

namespace WebApplication1.Controllers
{
    
    public class DefaultController : Controller
    {
        private static Game game = new Game();

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
     
   
        public ActionResult Button(string mark)
        {
            string[] values = mark.Split(',');


            var isOk =  game.PlaceMark(Convert.ToInt32(values[1]), Convert.ToInt32(values[0]));
            System.Diagnostics.Debug.WriteLine(game.PrintGameBoard());
            if (!isOk)
            {
                //Write error message in viewbag? or nothing happends??
            }
            return Redirect("Index");
        }

        //public ActionResult Sida2()
        //{
        //    ////Class1 myClass = new Class1();
        //    //int result = myClass.AddNumbers(2,2);
        //    //return View();
        //}
    }
}