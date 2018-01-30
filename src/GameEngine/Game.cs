using System;
using System.Web.UI.WebControls;

namespace GameEngine
{
    /// <summary>
    /// Class that handles the logic for the game Tic-tac-toe, mainly handling how an actual gameboard functions and
    /// what the rules in the game are.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Property that is a two-dimensional array that is to contain values of
        /// the Enum type "Mark". Representing the gameboard of the Game.
        /// </summary>
        private Mark[,] gameBoard;

        /// <summary>
        /// Here we create a new type called Mark that we can use to distinguish between the player
        /// with player mark X, the player with player mark O and "Nobody"-when there is no player mark
        /// on a certain field of the board. The property GameBoard is a two-dimensional array of the 
        /// created enum type "Mark".
        /// </summary>
        public enum Mark
        {
            Nobody,
            PlayerX,
            PlayerO
        }

        /// <summary>
        /// property that returns the Mark that is currentPlayer in a specific state of the game.
        /// </summary>
        public Mark CurrentPlayer
        {
            get
            { 
                return currentPlayer;
            }
        }
        /// <summary>
        /// Field used to store currentPlayer. Initially set to "PlayerX" since the first player
        /// is always PlayerX.
        /// </summary>
        private Mark currentPlayer = Mark.PlayerX;

        /// <summary>
        /// Constructor that instantiate a new gameboard.
        /// In the beginning of the game the board has no player marks on it,
        /// this methods therefore construct the empty board for the start of the game.
        /// </summary>
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
        /// <summary>
        /// Method that changes which player's turn it is, shifting from one player to
        /// the other depending on who's turn it is (who is set as the currentplayer 
        /// at the moment).
        /// </summary>
        private void ChangePlayerTurn()
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

        /// <summary>
        /// Method that checks if a certain field (box) on the game board is free (meaning: "Nobody" is the Mark
        /// currently found on the field.
        /// </summary>
        /// <param name="x">The x coordinate (column) for the field that we are checking on the board</param>
        /// <param name="y">The y coordinate (row) for the field that we are checking on the board</param>
        /// <returns>True if the Mark found on the field is "Nobody", otherwise false</returns>
        public bool IsFree(int x, int y)
        {
            return GetMarkAt(x, y) == Mark.Nobody;
        }

        /// <summary>
        /// Method that places a mark at a field on the gameboard, if the game doesn't yet have a winner
        /// and if the field is free the mark of the currentPlayer is placed on the field. Then method
        /// ChangePlayerTurn is called.
        /// </summary>
        /// <param name="x">The x coordinate (column) where we want to place a mark.</param>
        /// <param name="y">The y coordinate (row) where we want to place a mark.</param>
        /// <returns>True if it was possible to place the mark on the specified field, otherwise false.</returns>
        public bool PlaceMark(int x, int y)
        {
            if (!HasWinner() && IsFree(x, y))
            {
                gameBoard[y, x] = currentPlayer;
                ChangePlayerTurn();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method that returns the mark located on a specific field on the gameBoard.
        /// </summary>
        /// <param name="x">The x coordinate (column) that we want to fetch the mark from.</param>
        /// <param name="y">The y coordinate (row) that we want to fetch the mark from.</param>
        /// <returns>The mark located on the specified field on the board.</returns>
        public Mark GetMarkAt(int x, int y)
        {
            return gameBoard[y, x];
        }

        /// <summary>
        /// Method that controls if all the fields on the gameboard is full of Player Marks, or if any
        /// fields are empty. Returns true if no field is empty from any player Mark.
        /// </summary>
        /// <returns>True if board is full- no field on the gameboard is free, otherwise false.</returns>
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

        /// <summary>
        /// Method that checks if there is a winner on any of the rows on the game board. Iterates
        /// over each of the three rows, within the loop the method "WinnerOnRow" is called- 
        /// checks each individual row for similar marks.
        /// </summary>
        /// <returns>winner if any of the rows contain three similar marks that are not "Nobody", 
        /// otherwise returns Nobody (meaning-no win on rows)</returns>
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

        /// <summary>
        /// Method that checks if there is a winner on any column of the gameboard.  Iterates
        /// over each of the three columns, within the loop the method "WinnerOnColumn" is called- 
        /// checks each individual column for similar marks.
        /// </summary>
        /// <returns>winner if any of the columns contain three similar marks that are not "Nobody", 
        /// otherwise returns Nobody (meaning-no win on columns) </returns>
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

        /// <summary>
        /// Method that checks if there is a winner on any of the two diagonals on the game board, 
        /// meaning: if there are three similar Marks on any of the two diagonals.
        /// </summary>
        /// <returns>The Mark of the winner (X or O), otherwise Nobody</returns>
        private Mark WinnerOnDiagonals()
        {
            if ((gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2]) || (gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0]))
            {
                return gameBoard[1, 1];
            }
            return Mark.Nobody;
        }
        /// <summary>
        /// Method that checks individual rows for three similar player Marks, meaning there is a winner.
        /// </summary>
        /// <param name="y">The row that we want to check for wins</param>
        /// <returns>The mark that is similar in every field of the specified row (y)</returns>
        private Mark WinnerOnRow(int y)
        {
            if (gameBoard[y, 0] == gameBoard[y, 1] && gameBoard[y, 1] == gameBoard[y, 2])
            {
                return gameBoard[y, 0];
            }
            return Mark.Nobody;
        }

        /// <summary>
        /// Method that checks individual columns for three similar player Marks.
        /// </summary>
        /// <param name="x">The column that we want to check for wins</param>
        /// <returns>The mark that is similar in every field of the specified column (x)</returns>
        private Mark WinnerOnColumn(int x)
        {
            if (gameBoard[0, x] == gameBoard[1, x] && gameBoard[1, x] == gameBoard[2, x])
            {
                return gameBoard[0, x];
            }
            return Mark.Nobody;
        }

        /// <summary>
        /// Method that checks if the game has a winner. Calls the method "WhoisWinner".
        /// </summary>
        /// <returns>True if the winner is PlayerX or PlayerO, otherwise false.</returns>
        public bool HasWinner()
        {
            return WhoIsWinner() != Mark.Nobody;
        }

        /// <summary>
        /// Method that controls who the winner of the game is.
        /// </summary>
        /// <returns>The Mark of the winner.</returns>
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

        /// <summary>
        /// Debug method to test that the correct gameboard is rendered.
        /// </summary>
        /// <returns>A string representation of the gameBoard at a given state</returns>
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
    }
}
