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
        // private static GameSession game = new GameSession();
        // GET: Default
        private static Random idGenerator = new Random();
        private static Dictionary<int, GameSession> GameSessions = new Dictionary<int, GameSession>();

        //Lista som byggs upp för att skicka vidare till viewen.
        [Route("")]
        public ActionResult FirstPage()
        {
            Player currentPlayer = (Player)Session["player"];
            if (currentPlayer != null)
            {
                if(!GameSessions[currentPlayer.GameID].GameOver())
                {
                    return RedirectToBoard(currentPlayer.GameID);
                }
            }
            List<GameSession> openGames = new List<GameSession>();
            foreach (GameSession game in GameSessions.Values)
            {
                if (!game.GameFull)
                {
                    openGames.Add(game);
                }
            }
            return View("FirstPage", openGames);
        }

        public ActionResult JoinGame(string playerOName, int ? id)
        {
            if (id != null)
            {
                GameSession game = GameSessions[(int)id];
                Player secondPlayer = new Player
                {
                    NickName = playerOName,
                    PlayerID = idGenerator.Next(),
                    MarkId = Game.Mark.PlayerO,
                    GameID = (int)id
                };
                game.JoinGame(secondPlayer);
                game.StartGame();
                Session["player"] = secondPlayer;
                return RedirectToBoard((int)id);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult CreateGame(string playerXName)
        {
            System.Diagnostics.Debug.WriteLine("Creating game for player " + playerXName);
            int gameID = idGenerator.Next();
            GameSession newGame = new GameSession(gameID);
            GameSessions.Add(gameID, newGame);
            Player firstPlayer = new Player { NickName = playerXName, GameID = gameID, MarkId = Game.Mark.PlayerX, PlayerID = idGenerator.Next() };
            newGame.JoinGame(firstPlayer);
            Session["player"] = firstPlayer;
            return RedirectToBoard(gameID);
        }

        //To be able to look at a specific game we need to have an ID for the game as a parameter to the Actionresult.
        public ActionResult ShowGameBoard(int id)
        {
            GameSession game = GameSessions[id];
            /*if (!string.IsNullOrEmpty(mark))
            {
                ViewBag.Result = "X";
                ViewBag.Button = mark;
            }*/
            System.Diagnostics.Debug.WriteLine("Showing the game board for game " + id);
            System.Diagnostics.Debug.WriteLine(game.SpecificGame.PrintGameBoard());
            return View("ShowGameBoard", game);
        }
        public ActionResult PlaceMark(int id, string coordinates)
        {
            GameSession game = GameSessions[id];
            /*ViewBag.Result = "X";
            ViewBag.Button = mark;
            return View();*/
            if (((Player)Session["player"]).MarkId != game.SpecificGame.CurrentPlayer)
            {
                return RedirectToBoard(id);
            }
            if(!game.GameFull)
            {
                return RedirectToBoard(id);
            }
            System.Diagnostics.Debug.WriteLine("Placing mark at coordinates " + coordinates);
            string[] values = coordinates.Split(',');

            var isOk = game.SpecificGame.PlaceMark(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));

            if (!isOk)
            {
                // show some sort of error message view?
            }
            return RedirectToBoard(id);
        }

        private RedirectResult RedirectToBoard(int id)
        {
            return Redirect("/GameSession/ShowGameBoard/" + id.ToString());
        }

        public ActionResult StartPage()
        {

            return View();
        }

        //public ActionResult Sida2()
        //{
        //    ////Class1 myClass = new Class1();
        //    //int result = myClass.AddNumbers(2,2);
        //    //return View();
        //}
    }
}