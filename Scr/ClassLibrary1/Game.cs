using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Game
    {
        private int[,] boardArea = new char[3, 3];

        //Property that defines the boardarea: a 3x3 dimensional board.
        public int[,] BoardArea
        {
            get
            {
                return boardArea;
            }
            set
            {
                boardArea = value;
            }
        }

        /*public enum Player
        {
            X,
            O
        }*/




    }
}
