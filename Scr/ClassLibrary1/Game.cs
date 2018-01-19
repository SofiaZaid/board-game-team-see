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
        public int place; 
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
        public Game(int xRow)
        {
            for (int i = 0; i < xRow * xRow; i++)
            {
                spot.Add(new Spot { player = null, place = i });
            }

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
