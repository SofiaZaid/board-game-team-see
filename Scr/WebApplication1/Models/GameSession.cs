using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameEngine;

//Webbackend for tic-tac toe. GameSession blir värdet, nyckeln blir ett sessionsID.
namespace WebApplication1.Models
{
    public class GameSession
    {
        public Game SpecificGame { get; set; }
        public int GameId { get; set; }
        public Player[] PlayersInSpecificGame { get; set; }
    }
}