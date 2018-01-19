using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Spot
    {
        public Player player;
        public int x;
        public int y;
    }

    class Game
    {
        // Här skapar vi  av platser som ma kan placera en markör på. Listan innehåller 
        // vilken plats det är och vilken spelare som står på den. 

        /// <summary>
        /// Detta är en kontrukor som skapar brädet som vi ska spela på. Bärdet är tomt alla platser är lediga. under spelets gång kommer objekten i listan 
        /// att uppdaters. T.ex. när en spelare placerar en markör på t.ex spot[4] så blir den rutan upptagen och rutan tillhör nu spelaren som klickade på den. 
        /// </summary>
        public static List<Spot> spot = new List<Spot>();
        public Game()
        {            

            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x< 3; x++)
                {
                    spot.Add(new Spot { player = null, x = i, y = x });
                }
            }

        }

        //Metod som kontrollerar om det är möjligt att lägga sin spelmarkör på ett specifikt fält på spelbrädet.
        //Argumenten som metoden tar in är en x och en y - koordinat i rutnätet som utgör spelplanen.
        public bool CheckIfFieldOnBoardIsFree(List<Spot> x, int place)
        {
            if (x[place].player == null)
            {
                return true;
            }

            return false;
        }
            

        //Metod som sköter själva handlingen att placera ut en pjäs på brädet, i en speciell ruta.

        public void PlaceGamePiece(List<Spot> x, int place)
        {

            if (CheckIfFieldOnBoardIsFree(spot, place))
            {
                // placera markör
            }
            else
            {
                // Alert spot i taken
            }

        }

       

        //Method that controls if there are three similar game pieces/player marks in a row on the board. If So, return true.
        public bool CheckForWinOnRows(List<Spot> spots)
        {
            for (int i = 0; i < 3; i++)
            {
               
            }
            return false;
        }

        //Method that controls if there are three similar game pieces/player marks on the diagonal on the board. If So, return true.
        public bool CheckForWinsOnDiagonal()
        {
            return true;
        }

        //Method that controls if there are three similar game pieces/player marks in a column on the board. If So, return true.
        public bool CheckForWinsOnColumns()
        {
            return true;
        }

        //Method that controls if any of the "CheckForWinsOn..."-methods are evaluated to true. If so, the game has a winner and it is over.
        public bool CheckIfGamehasAWinner()
        {
            return true;
        }

        //Method that controls who the current player is, if current player is X then the value of currentPlayer is changed to O, and
        //the other way around in the opposite case. Method is used to make it possible to switch turns in the game.
        public void ChangePlayerTurn()
        {


        }

        public bool CheckValueSimilarityInRow(char piece1, char piece2, char piece3)
        {
            return ((piece1 != '-') && (piece1 == piece2) && (piece2 == piece3));
        }
        public Player WinnerOfGame()
        {
            //nedan kod är ej korrekt, bara för att få bort rödmarkeringen så länge.
            Player p1 = new Player();
            return p1;
        }

    }
}
