using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SudokuControlLib
{
    public partial class custSquare : Control
    {

        private Color backColor;
        private Color foreColor;
        private Size clientSize;
        private string text;

        public Color custBackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                Invalidate();
            }
        }

        public Color custForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
                Invalidate();
            }
        }


        public Size custSize
        {
            get { return clientSize; }
            set
            {
                clientSize = value;
                Invalidate();
            }
        }

        public string custText
        { get; set; }

        public custSquare()
        {
            InitializeComponent();


            // setting the actual values on initialization,
            // can be changed through public properties
            ForeColor = Color.Black;
            BackColor = Color.White;
            ClientSize = new Size(100, 100);
            Font = new System.Drawing.Font("Times New Roman", 12.01F);
            Text = "[Initial Text]";

            //

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Brush brush =
                new HatchBrush(HatchStyle.DarkDownwardDiagonal,
                custBackColor, custForeColor);

            Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(
                ClientRectangle, backColor, foreColor, 1.8F);


            Brush b3 = System.Drawing.SystemBrushes.Desktop;
            Font f1 = new System.Drawing.Font("Apple Garamond", 12);
            //pe.Graphics.
            pe.Graphics.FillRectangle(b3, ClientRectangle);
            brush.Dispose();
        }
    }
}
