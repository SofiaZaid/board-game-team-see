using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //Ett sätt att flytta ut logiken till ett annat ställe. Få ett mer modulärt tänk.
    public class GameMethod
    {

        public bool ISWin()
        {
            return CheckForWinOnRows() && CheckForWinsOnDiagonal() && CheckForWinsOnColumns();

        }

        //Metod som sköter själva handlingen att placera ut en pjäs på brädet, i en speciell ruta.
        public void PlaceGamePiece(int toX, int toY)
        {
            if (CheckIfFieldOnBoardIsFree(1, 1))
            {

            }

        }

        //Metod som kontrollerar om det är möjligt att lägga sin spelmarkör på ett specifikt fält på spelbrädet.
        //Argumenten som metoden tar in är en x och en y - koordinat i rutnätet som utgör spelplanen.
        public bool CheckIfFieldOnBoardIsFree(int x, int y)
        {
            return true;
        }

        //Method that controls if there are three similar game pieces/player marks in a row on the board. If So, return true.
        public bool CheckForWinOnRows()
        {
            for (int i = 0; i < 3; i++)
            {

            }
            return true; 
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

    }
}
