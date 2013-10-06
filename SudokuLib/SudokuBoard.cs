using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuLib
{

    // this should only be instantiated once in the game
    public class SudokuBoard
    {
        public int numSquares = 81;
        public Square square;
        //public int[][] ps;
        public SquareCollection squaresCollection;

        public SudokuBoard(int size)
        {
            numSquares = size;
            squaresCollection = new SquareCollection();
        }

        public SudokuBoard()
        {

            squaresCollection = new SquareCollection();
            
            // create 2 dimensional array of positions
      
            square = new Square();
            int numX = 0;
            int numY = 0;

            for (int i = 0; i < numSquares; i++)
            {
                square = new Square();
                square.Number = 0;


                square.Position.X = numX;
                square.Position.Y = numY;

                // increment y until it reaches 8
                if (numY < 9)
                {
                    numY++;
                }


                if (numY == 9 && numX < 8)
                {
                    numX++;
                    numY = 0;
                }

                // add to the collection
                squaresCollection.Add(square);
                
            }

            //squaresCollection.Item(0).Number = 1;//1
            //squaresCollection.Item(3).Number = 1;//4
            //squaresCollection.Item(8).Number = 1;
            //squaresCollection.Item(9).Number = 1;
            //squaresCollection.Item(11).Number = 1;
        }


        //public void InitializeBoard(Square[] squares)
        //{
        //    int[,] sq = new int[9,9];

        //    for (int i = 0; i < 9; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            sq[i,j] = 0;
        //        }
        //    }

        //    }

            

        }

        
    }
