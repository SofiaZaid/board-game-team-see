using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    class Game
    {
        //Here we create a new type called Mark that we can use to distinguish between the player
        //with player mark X, the player with player mark O and "Nobody"-when there is no player mark
        //on a certain field of the board. The property GameBoard is a two-dimensional array of the 
        //created enum type "Mark".
        public Mark[,] gameBoard;
        public enum Mark
        {
            Nobody,
            PlayerX,
            PlayerO
        }

        //Constructor that instansiate a new gameboard.
        //In the beginning of the game the board has no player marks on it,
        //this methods therefore construct the empty board for the start of the game.
        public Game()
        {
            gameBoard = new Mark[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = Mark.Nobody;
                }
            }
        }

        //Method that checks if a certain field (box) on the game board is free (meaning: "Nobody" is the Mark
        //on the field. We are checking a special position on the board: y marks the row and x marks the column
        private bool IsFree(int x, int y)
        {
            return gameBoard[y, x] == Mark.Nobody;
        }

        //Modifiera så att denna ej tar in spelarargumentet, har metoden changeplayerturn för det.
        //Method that places a player mark on the board. The arguments specifices which row and column
        //the specific field is located on, and the player argument specifices which player it is that
        //wants to place her mark on the board. Calls method "IsFree" to firstly check if the field is 
        //free, if so- places the player's mark there. Else- throws an exception.
        public void PlaceMark(int x, int y, Mark player)
        {
            if (IsFree(x, y))
            {
                gameBoard[y, x] = player;
            }
            else
            {
                throw new InvalidOperationException("This field on the board is already occupied, action invalid.");
            }
        }


        //Method to be implemented- we could let the Game keep check on who's turn it is. If implemented, Method
        //PlaceMark will not need to get the "Mark player" as an argument. The Game then keeps check on whos turn
        //it is
        public void ChangePlayerTurn()
        {

        }

        //Method that checks if there is a winner on any of the rows on the game board. Since there are three rows we need to
        //iterate over each of them, within the loop the method "WinnerOnRow" is called- it checks each individual row for
        //similar marks. Returns the player (X or O if there are a three similar Marks on any row, else returns Mark "Nobody".
        private Mark WinnerOnRows()
        {
            for (int i = 0; i < 3; i++)
            {
                Mark winner = WinnerOnRow(i);
                if (winner != Mark.Nobody)
                {
                    return winner;
                }
            }
            return Mark.Nobody;
        }

        //Method that checks if there is a winner on any of the columns on the game board. Since there are three columns we need to
        //iterate over each of them, within the loop the method "WinnerOnColumn" is called- it checks each individual column for
        //similar marks. Returns the player (X or O if there are a three similar Marks on any column, else returns Mark "Nobody".
        private Mark WinnerOnColumns()
        {
            for (int i = 0; i < 3; i++)
            {
                Mark winner = WinnerOnColumn(i);
                if (winner != Mark.Nobody)
                {
                    return winner;
                }
            }
            return Mark.Nobody;
        }

        //Method that checks if there is a winner on any of the two diagonals on the game board, meaning: if there are three similar Marks on
        //any of the two diagonals. Returns the player Mark X or O on any of the positions if this is the case. Else returns Mark.Nobody- there 
        //wasn't three similar Marks on any of the diagonals.
        private Mark WinnerOnDiagonals()
        {
            if ((gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2]) || (gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0]))
            {
                return gameBoard[1, 1];
            }
            return Mark.Nobody;
        }

        //Method that checks individual rows for three similar player Marks.
        private Mark WinnerOnRow(int y)
        {
            if (gameBoard[y, 0] == gameBoard[y, 1] && gameBoard[y, 1] == gameBoard[y, 2])
            {
                return gameBoard[y, 0];
            }
            return Mark.Nobody;
        }

        //Method that checks individual columns for three similar player Marks.
        private Mark WinnerOnColumn(int x)
        {
            if (gameBoard[0, x] == gameBoard[1, x] && gameBoard[1, x] == gameBoard[2, x])
            {
                return gameBoard[0, x];
            }
            return Mark.Nobody;

        }

        //Method that calls the method WhoIsWinner. This method is used to check if the game has any winner yet.
        public bool HasWinner()
        {
            return WhoIsWinner() != Mark.Nobody;
        }

        //Method that controls who the winner of the game is. Initially the winner variable is set to Nobody. 
        //Then we call the method WinnerOnDiagonals, if the winner isn't Nobody after this we return the winner:
        //X or O. If the winner was still Nobody, we call the method WinnerOnColumns, if we instead got a winner
        //on one of the columns we then return the winner. If the winner still again was Nobody we return the
        //result of the method WinnerOnRows.
        public Mark WhoIsWinner()
        {
            Mark winner = Mark.Nobody;
            winner = WinnerOnDiagonals();
            if (winner != Mark.Nobody)
            {
                return winner;
            }
            winner = WinnerOnColumns();
            if (winner != Mark.Nobody)
            {
                return winner;
            }
            return WinnerOnRows();
        }

        //Test method- not to be kept in this code later on. Just to be able to test and see that we can get a correct gameboard printed out.
        public void PrintGameBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (gameBoard[i, j])
                    {
                        case Mark.PlayerX:
                            Console.Write("X");
                            break;
                        case Mark.PlayerO:
                            Console.Write("O");
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
