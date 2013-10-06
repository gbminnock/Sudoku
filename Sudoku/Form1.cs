using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SudokuLib;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        int sWidth = 70;
        int sHeight = 70;
        int selectedValue;

        public Form1()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.WindowState = FormWindowState.Maximized;

            // FULLSCREEN
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;


            SudokuBoard board = new SudokuBoard();
            Panel pBoard;
            Panel s1;
            Label l1;
            Label l2;
            TransparentLabel l3;


            pBoard = new Panel();
            pBoard.Height = (sWidth * 9);
            pBoard.Width = (sHeight * 9);
            pBoard.AutoSize = true;
            pBoard.BackColor = Color.Black;
            pBoard.Location = new Point(70, 50);
            pBoard.BorderStyle = BorderStyle.FixedSingle;
            pBoard.Name = "Board";
            this.Controls.Add(pBoard);

            // positioning squares on the board
            foreach (Square square in board.squaresCollection)
            {
                s1 = new Panel();
                s1.Height = sWidth;
                s1.Width = sHeight;
                
                
                s1.Location = new Point(square.Position.Y * sHeight, square.Position.X * sWidth);

                if (square.Position.Y == 3 || square.Position.Y == 6)
                {
                    s1.Location = new Point((square.Position.Y * sHeight ) + 2, square.Position.X * sWidth);
                }

                if (square.Position.X == 3 || square.Position.X == 6)
                {
                    s1.Location = new Point(square.Position.Y * sHeight, (square.Position.X * sWidth) + 2);
                }

                if ((square.Position.X == 3 || square.Position.X == 6) && (square.Position.Y == 3 || square.Position.Y == 6))
                {
                    s1.Location = new Point((square.Position.Y * sHeight) + 2, (square.Position.X * sWidth) + 2);
                }

                s1.BackColor = Color.White;
                s1.BorderStyle = BorderStyle.FixedSingle;

                #region old
                //s1.MouseDown += new MouseEventHandler(s1_MouseDown);
                //s1.MouseUp += new MouseEventHandler(s1_MouseUp);
                //s1.MouseDoubleClick += new MouseEventHandler(s1_MouseDoubleClick);
                #endregion

                s1.MouseClick += new MouseEventHandler(s1_MouseClick);
                s1.Enabled = true;
                pBoard.Controls.Add(s1);

                l1 = new Label();
                //l1.Text = square.Number.ToString();
                l1.ForeColor = Color.Gray;
                l1.BackColor = Color.Transparent;
                s1.Controls.Add(l1);
                var fSize = l1.Height;

                l2 = new Label();
                l2.Top = 50;
                l2.ForeColor = Color.Gray;
                l2.BackColor = Color.Transparent;
                //l2.Text = "(" + square.Position.X.ToString() + ", " + square.Position.Y.ToString() + ")";
                s1.Controls.Add(l2);

                l3 = new TransparentLabel();
                if (square.Number != 0) // we don't want to show any zeros
                {
                    l3.Text = square.Number.ToString();
                }
                else
                {
                    l3.Text = "";
                }
                Font currentFont = l3.Font;
                l3.Top = 10;
                l3.Height = 50;
                l3.Width = 70;
                l3.Name = "label";
                l3.Font = new Font(currentFont.FontFamily, 30);
                
                s1.Controls.Add(l3);
                l3.BringToFront();
                l1.SendToBack();
                l2.SendToBack();
                l1.Enabled = false;
                l2.Enabled = false;
                l3.Enabled = false;

                s1.BringToFront();
            }

            // side selection number list
            NumberList();
            addSolveButton();

            // Solve Button

        }

        void s1_MouseClick(object sender, MouseEventArgs e)
        {
            
            Panel board = this.Controls.Find("Board", true).FirstOrDefault() as Panel;

            // get all the squares on the board
            var squares = board.Controls.OfType<Panel>();


            // turn all squares to white
            foreach (Panel p in squares)
            {
                p.BackColor = Color.White;
            }


            Panel numberList = this.Controls.Find("NumberList", true).FirstOrDefault() as Panel;

            // get all the squares on the board
            var numSquares = numberList.Controls.OfType<Panel>();


            // turn all squares to white
            foreach (Panel p in numSquares)
            {
                p.BackColor = Color.White;
            }


            // now turn on color for this square
            Panel sq = (Panel)sender;
            if (sq.BackColor == Color.Yellow)
            {
                sq.BackColor = Color.White;
            }
            else
            {
                if (selectedValue != 0)
                {
                    TransparentLabel l1 = (TransparentLabel)sq.Controls.Find("label", false).FirstOrDefault() as TransparentLabel;
                    l1.Text = selectedValue.ToString();
                }
                selectedValue = 0;
                sq.BackColor = Color.Yellow;
            }
        }

        void addSolveButton()
        {
            Panel board = this.Controls.Find("Board", true).FirstOrDefault() as Panel;

            Point p = new Point(board.Location.X, board.Location.Y);

            Button btnSolve = new Button();
            btnSolve.Text = "Solve";
            //btnSolve.Width = 100;
            //btnSolve.Height = 100;
            btnSolve.Click += new EventHandler(btnSolve_Click);
            btnSolve.Enabled = true;
            btnSolve.Location = new Point(800,400);

            
            this.Controls.Add(btnSolve);

        }

        void btnSolve_Click(object sender, EventArgs e)
        {
            // must get a 2 dimensional array of the grid/board
            Panel pboard = this.Controls.Find("Board", true).FirstOrDefault() as Panel;
            SudokuBoard board = new SudokuBoard(81);

            // get all the squares on the board
            List<Panel> squares = pboard.Controls.OfType<Panel>().ToList();
            int[,] grid = new int[9, 9];

            for(int i=0; i < squares.Count; i++)
            {
                var square = squares[i];
                TransparentLabel l1 = (TransparentLabel)square.Controls.Find("label", false).FirstOrDefault() as TransparentLabel;
                board.squaresCollection.Add(
                    new Square(){ Number = l1.Text == String.Empty ? 0 : Convert.ToInt32(l1.Text) });
            }

            int ik = 0;
        }

        void NumberList()
        {
            Panel board = this.Controls.Find("Board", true).FirstOrDefault() as Panel;
            int x = board.Location.X + board.Width + 100;
            int y = board.Location.Y;

            Panel pNumList = new Panel();
            pNumList.Name = "NumberList";
            pNumList.Width = sWidth/2;
            pNumList.Height = (sHeight * 9)/2;
            pNumList.BorderStyle = BorderStyle.FixedSingle;
            pNumList.BackColor = Color.Black;
            pNumList.Location = new Point(x, y);


            Label l1;
            Panel pNum;

            for(int i = 0; i < 9; i++)
            {
                // label
                l1 = new Label();
                l1.Name = "label";
                l1.Text = (i + 1).ToString();
                l1.Left = 4;
                Font currentFont = l1.Font;
                l1.Font = new Font(currentFont.FontFamily, 15);
                l1.Enabled = true;
                l1.ForeColor = Color.Black;
                
                // square
                pNum = new Panel();
                pNum.Height = sHeight / 2;
                pNum.Width = sWidth / 2;
                pNum.Location = new Point(0,i * (sHeight/2));
                pNum.BorderStyle = BorderStyle.FixedSingle;
                pNum.BackColor = Color.White;
                pNum.ForeColor = Color.Black;
                pNum.MouseClick += new MouseEventHandler(pNum_MouseClick);
                pNum.Enabled = true;
                pNum.Controls.Add(l1);


                pNum.BringToFront();
                pNum.Enabled = true;
                l1.Enabled = false;
                l1.SendToBack();
                pNumList.Controls.Add(pNum);

            }

 
            this.Controls.Add(pNumList);

        }

        void pNum_MouseClick(object sender, MouseEventArgs e)
        {

            Panel board = this.Controls.Find("Board", true).FirstOrDefault() as Panel;

            // get all the squares on the board
            var squares = board.Controls.OfType<Panel>();


            // turn all squares to white
            foreach (Panel p in squares)
            {
                p.BackColor = Color.White;
            }


            Panel numberList = this.Controls.Find("NumberList", true).FirstOrDefault() as Panel;

            // get all the squares on the board
            var numSquares = numberList.Controls.OfType<Panel>();


            // turn all squares to white
            foreach (Panel p in numSquares)
            {
                p.BackColor = Color.White;
            }

            // now turn on color for this square
            Panel sq = (Panel)sender;
            if (sq.BackColor == Color.Yellow)
            {
                sq.BackColor = Color.White;
            }
            else
            {
                Label l1 = (Label)sq.Controls.Find("label",false).FirstOrDefault() as Label;
                selectedValue = Convert.ToInt32(l1.Text);
                sq.BackColor = Color.Yellow;
            }
        }

        #region old code
        //void s1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    Panel sq = (Panel)sender;
        //    sq.BackColor = Color.Red;
        //}

        //void s1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Clicks == 1)
        //    {
        //        Panel sq = (Panel)sender;
        //        sq.BackColor = Color.White;
        //    }
        //}


        //void s1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Panel sq = (Panel)sender;
        //    sq.BackColor = Color.Yellow;
        //}
        #endregion

    }
}
