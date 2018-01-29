using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameEngine;

//Webbackend for tic-tac toe. GameSession blir värdet, nyckeln blir ett sessionsID.
namespace TicTacToe.Models
{
    public class GameSession
    {
        public Game SpecificGame { get; }
        public List<Player> PlayersInSpecificGame { get; }
        public int GameID { get; }
        public bool GameFull
        {
            get
            {
                return PlayersInSpecificGame.Count >= 2;
            }
        }

        private State currentState = State.Waiting;
        public enum State
        {
            Waiting,
            Started
        }

        public GameSession(int gameID)
        {
            SpecificGame = new Game();
            GameID = gameID;
            PlayersInSpecificGame = new List<Player>();
        }

        public void JoinGame(Player p)
        {
            if (!GameFull && currentState == State.Waiting)
            {
                PlayersInSpecificGame.Add(p);
            }
        }
        
        public void StartGame()
        {
            if(GameFull && currentState == State.Waiting)
            {
                currentState = State.Started;
            }
        }


        public bool GameOver()
        {
            return (!GameFull && currentState == State.Started) || SpecificGame.HasWinner() || SpecificGame.IsBoardFull();
        }
    }
}