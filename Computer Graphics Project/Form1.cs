using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Graphics_Project
{
    public class LineSegment
    {
        public PointF ptS, ptE;
        Pen pen = new Pen(Color.Red, 5);
        public Transformation trans = new Transformation();
        public int move = 0;

        public void DrawYourSelf(Graphics g)
        {
            g.DrawLine(pen, ptS.X, ptS.Y, ptE.X, ptE.Y);
            g.FillEllipse(Brushes.Gray, ptS.X - 10, ptS.Y - 10, 20, 20);
            g.FillEllipse(Brushes.Gray, ptE.X - 10, ptE.Y - 10, 20, 20);
        }
    }

    public partial class Form1 : Form
    {
        Timer T = new Timer();
        Bitmap image;
        int centerX, centerY;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            T.Tick += T_Tick;
            T.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {


            drawdubb(this.CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            centerX = this.Width / 2;
            centerY = this.Height / 2;
            image = new Bitmap("wallpaper.jpg");
        }

        private void drawscene(Graphics g2)
        {
            //g2.Clear(Color.White);
        }

        private void drawdubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(image);
            drawscene(g2);
            g.DrawImage(image, 0, 0, this.Width, this.Height);
        }
    }
}
