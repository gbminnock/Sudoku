using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SudokuLib
{
    public class Scratch
    {

        #region vars

        protected int[,] board;
        protected int boardSize = 18;
        protected const int UNASSIGNED = 0;

        #endregion

        // Start the program
        public Scratch()
        {
            board = new int[boardSize, boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    board[row, column] = UNASSIGNED;
                }
            }

            // setup for testing
            board[0, 0] = 9;
            board[8, 8] = 7;
            board[8, 0] = 6;
            board[0, 8] = 5;


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            BackTrack();
            stopwatch.Stop();;
            Console.WriteLine("Backtracker took " + stopwatch.Elapsed.ToString() + " to complete.");

        }


        /* 
         * Backtracking using recursion 
         */ 
        protected bool BackTrack()
        {

            int row = 0;
            int col = 0;

            /* check if all positions on the board have been filled, if the have the the puzzle is solved
             * Use ref variables so that we don't have to loop throup each time. this function will
             * assign each value of row and column for us automatically.
             */ 
            if (!FindUnassignedLocation(ref row, ref col))
            {
                return true;
            }

            // loop through possible values
            for (int value = 1; value <= boardSize; value++)
            {

                if (!IsConflicts(row, col, value))
                {
                    board[row, col] = value;
                    outBoard();
                    if (BackTrack()) return true;
                    board[row, col] = UNASSIGNED;
                }
            }
            return false; //triggers backtracking from earlier decision
        }

        /*
         * Displays the board and outputs to the file for tracing and debugging
         */ 
        public void outBoard()
        {
            using (StreamWriter s = new StreamWriter("output.txt", true))
            {
                Console.WriteLine();

                for (int row = 0; row < boardSize; row++)
                {
                    for (int col = 0; col < boardSize; col++)
                    {
                        Console.Write(board[row, col] + " ");
                        s.Write(board[row, col] + " ");
                    }
                    Console.WriteLine();
                    s.WriteLine();
                }
                s.WriteLine();
                Console.WriteLine();
            }
        }

        /*
         * Groups Horizontal, Vertical and Square checks together
         */ 
        protected bool IsConflicts(int row, int col, int value)
        {
            // used variables here for readability
            bool horz = IsConflictHorizontal(row, value);
            bool vert = IsConflictVertical(col, value);
            bool square = IsConflictSquare(row-row%3, col-col%3, value);

            return horz || vert || square;
        }

        /*
         * Checks if a value exists in a certain row
         */
        protected bool IsConflictHorizontal(int row, int value)
        {
            // check each column in the row
            for (int col = 0; col < boardSize; col++)
            {
                if (board[row, col] == value)
                {
                    return true;
                }
            }
            return false;
        }
        
        /*
         * Checks if a value exists in a certain column
         */
        protected bool IsConflictVertical(int col, int value)
        {
            // check each row in the column
            for (int row = 0; row < boardSize; row++)
            {
                if (board[row, col] == value)
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * Checks the square of 3X3 for an existing value
         */ 
        protected bool IsConflictSquare(int subRow, int subCol, int value)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 0; column++)
                {
                    if (board[row + subRow, column + subCol] == value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*
         * Check that each location has a number
         * If it does the puzzle has been solved
         */
        protected bool FindUnassignedLocation(ref int row, ref int col)
        {
            // the row and col values get reassigned here.
            for (row = 0; row < boardSize; row++)
            {
                for (col = 0; col < boardSize; col++)
                {
                    if (board[row, col] == UNASSIGNED)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
