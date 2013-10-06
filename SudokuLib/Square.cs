using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SudokuLib
{
    // Icompareable used for sorting
    public class Square : IComparable<Square>

    {
        public int Number { get; set; }
        public Position Position { get; set; } // position on the board


        // initialize the square
        public Square()
        {
           Position = new Position();
        }

        // used for sorting
        public int CompareTo(Square other)
        {
            return this.Position.X.CompareTo(other.Position.X);
        }

    }
}
