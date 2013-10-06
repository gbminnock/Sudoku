using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuLib;
using System.Diagnostics;

namespace SudokuTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //SudokuEngine se = new SudokuEngine();

            /*Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Queens board = new Queens(5);
            board.solve();

            stopwatch.Stop();
            Console.WriteLine("Time taken: " + stopwatch.Elapsed.ToString());
             */

            int boardSize = 9;
            int[,] board = new int[boardSize,boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    board[row, col] = 0;
                }
            }

 
            SudokuEngine sudokuengine = new SudokuEngine(board);
            
            Console.ReadLine();
        }
    }
}
