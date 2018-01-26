using System;
using System.Web.UI.WebControls;

namespace GameEngine
{
    public class Game
    {
        //Here we create a new type called Mark that we can use to distinguish between the player
        //with player mark X, the player with player mark O and "Nobody"-when there is no player mark
        //on a certain field of the board. The property GameBoard is a two-dimensional array of the 
        //created enum type "Mark".
        public Mark[,] gameBoard;

        public int XPlusY(int x, int y)
        {
            return x + y;
        }

        public enum Mark
        {
            Nobody,
            PlayerX,
            PlayerO
        }

        public Mark CurrentPlayer
        {
            get
            { 
                return currentPlayer;
            }

        }
        private Mark currentPlayer = Mark.PlayerX;

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

        public void ChangePlayerTurn()
        {
            if (currentPlayer == Mark.PlayerX)
            {
                currentPlayer = Mark.PlayerO;
            }
            else if (currentPlayer == Mark.PlayerO)
            {
                currentPlayer = Mark.PlayerX;
            }
        }

        //Method that checks if a certain field (box) on the game board is free (meaning: "Nobody" is the Mark
        //on the field. We are checking a special position on the board: y marks the row and x marks the column
        public bool IsFree(int x, int y)
        {
            return GetMarkAt(x, y) == Mark.Nobody;
        }

        //Modifiera så att denna ej tar in spelarargumentet, har metoden changeplayerturn för det.
        //Method that places a player mark on the board. The arguments specifices which row and column
        //the specific field is located on, and the player argument specifices which player it is that
        //wants to place her mark on the board. Calls method "IsFree" to firstly check if the field is 
        //free, if so- places the player's mark there. Else- throws an exception.
        public bool PlaceMark(int x, int y)
        {
            if (!HasWinner() && IsFree(x, y))
            {
                gameBoard[y, x] = currentPlayer;
                ChangePlayerTurn();
                if (WinnerOnRows() != Mark.Nobody || WinnerOnColumns() != Mark.Nobody || WinnerOnDiagonals() != Mark.Nobody)
                {
                    //player won
                }

                return true;
            }
            //Maybe not an exception here?
            return false;
        }

        public Mark GetMarkAt(int x, int y)
        {
            return gameBoard[y, x];
        }

        //Method that controls if all the fields on the gameboard is full of Player Marks, or if any
        //fields are empty. Returns true if no field is empty from any player Mark.
        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (IsFree(i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
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
        public Mark WinnerOnRow(int y)
        {
            if (gameBoard[y, 0] == gameBoard[y, 1] && gameBoard[y, 1] == gameBoard[y, 2])
            {
                return gameBoard[y, 0];
            }
            return Mark.Nobody;
        }

        //Method that checks individual columns for three similar player Marks.
        public Mark WinnerOnColumn(int x)
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
        public string PrintGameBoard()
        {
            string boardString = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (gameBoard[i, j])
                    {
                        case Mark.PlayerX:
                            boardString += "X";
                            break;
                        case Mark.PlayerO:
                            boardString += "O";
                            break;
                        default:
                            boardString += " ";
                            break;
                    }
                }
                boardString += "\n";
            }
            return boardString;
        }
        public void OnClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Text = "...button clicked...";
        }
    }
}
