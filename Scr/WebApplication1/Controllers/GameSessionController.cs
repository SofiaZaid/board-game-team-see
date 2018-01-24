using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using GameEngine;

namespace WebApplication1.Controllers
{
    
    public class GameSessionController : Controller
    {
        private static Game game = new Game();
        // GET: Default


        public ActionResult ShowGameBoard(int? id)
        {
            /*if (!string.IsNullOrEmpty(mark))
            {
                ViewBag.Result = "X";
                ViewBag.Button = mark;
            }*/
            System.Diagnostics.Debug.WriteLine("Showing the game board for game " + id.ToString());
            System.Diagnostics.Debug.WriteLine(game.PrintGameBoard());
            return View("ShowGameBoard", game);
        }
        public ActionResult PlaceMark(int id, string coordinates)
        {

            /*ViewBag.Result = "X";
            ViewBag.Button = mark;
            return View();*/
            System.Diagnostics.Debug.WriteLine("Placing mark at coordinates " + coordinates);
            string[] values = coordinates.Split(',');
            
            var isOk = game.PlaceMark(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
            
            if (!isOk)
            {
                // show some sort of error message view?
            }
            //return Redirect("Index");
            return ShowGameBoard(id);
        }

        //public ActionResult Sida2()
        //{
        //    ////Class1 myClass = new Class1();
        //    //int result = myClass.AddNumbers(2,2);
        //    //return View();
        //}
    }
}