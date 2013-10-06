using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 
 * The Queens class. 
 * Places a number of chess queens on a board of a 
 * specified size so that no queen can attack another. 
 */ 

namespace SudokuLib
{
    

    public class Queens
    {
        protected int boardSize;
        protected bool[,] board;


        public Queens(int boardSize)
        {
            this.boardSize = boardSize;
            board = new bool[boardSize, boardSize];

            // creates a 9*9 board simply using a multidimensional array [9][9]
            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    // set each position to false; i.e. no queen exists there yet
                    board[row, column] = false;
                }
            }
        }



        /* 
        * pre: There are queens in columns 0 to (column - 1). 
        * post: Queens have been placed in all the columns 
        *       of board and PlaceQueens will return true, or 
        *       PlaceQueens will return false. 
        */ 

        // initially called by placeQueen(0) 
        protected bool placeQueen(int column)
        {
            int row;

            /*
             * if the column is the same as the boardSize i.e. 9 == 9 
             * then we can return because we have reached the end.
             * Otherwise we are going to check for threats. Can it be placed
             * same row, diagional up, diagional down?
             * 
             */
            if (column == boardSize)
            {
                return true;
            }
            else
            {
                bool successful = false;
                row = 0;

                /*
                 * the row starts at 0 
                 * keep looping until we reach the boardSize
                 * and successfull is false. 
                 * i.e. (5 < 9) AND (successful == false) 
                 */ 
                while ((row < boardSize) && !successful)
                {
                    /*
                     * if threat returns true then we should increment and the row i.e. check the next row
                     */
                    if (threat(row, column))
                    {
                        row++;
                    }
                    else
                    {
                        /* Place queen and try to place queen in next column. 
                         * we have been successful so now we will recurse to the next column
                         */
                        board[row, column] = true;
                        successful = placeQueen(column + 1);
                    

                        if (!successful)
                        {
                            // Remove the queen placed in the column. 
                            board[row, column] = false;
                            row++;
                        }
                    }
                }

                return successful;
            }
        }

        protected bool threat(int row, int column)
        {
            /* test for a queen in the same row
             * this could be row = 0, column = 0
             * Loop from (0 ... column)
             */ 
            for (int c = 0; c < column; c++)
            {
                /* This test each value in the 
                 * row and returns true if a 
                 * queen is found. i.e. board[row, c] will 
                 * return true if a queen exists in that
                 * position.
                 */ 
                if (board[row, c] )
                {
                    // found threat
                    return true;
                }
            }

            /* test for a queen in up diagional
             * why are we starting at 1?
             * loops from (1 ... column)
             */ 
            for (int c = 1; c <= column; c++)
            {
                if (row < c)
                {
                    break;
                }

                if (board[row - c, column - c])
                {
                    // found threat
                    return true;
                }
            }

            // Test for queen on down diagonal 
            for (int c = 1; c <= column; c++)
            {
                if (row + c >= boardSize)
                {
                    break;
                }

                if(board[row + c, column - c] )
                {
                    // found threat
                    return true;
                }
            }

            return false;
        } // end threat


        protected void outputBoard()
        {
            Console.WriteLine("Solution for board of size " + boardSize + ":"); 
            for (int row = 0; row < boardSize; row++) 
            { 
                for (int column = 0; column < boardSize; column++) 
                { 
                   if (board[row,column]) 
                   { 
                      Console.Write("Q "); 
                   } 
                   else 
                   { 
                      Console.Write(". "); 
                   } 
                } 
                 
                Console.WriteLine();
            }

            Console.WriteLine();
        } // end outputBoard


        public void solve() 
        { 
             if (placeQueen(0)) 
             { 
                outputBoard(); 
             } 
             else 
             { 
                Console.WriteLine("There is no solution possible"); 
             } 
        } // solve method 


    }
}
