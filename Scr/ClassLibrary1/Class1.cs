using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //Ett sätt att flytta ut logiken till ett annat ställe. Få ett mer modulärt tänk.
    public class Class1
    {
        public int AddNumbers(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}
