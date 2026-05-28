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
    public interface Road
    {
        void Draw(Graphics g);
        bool inside(int mouseX, int mouseY);
        void updatePos(int dx, int dy);
    }

    public class Loop: Road
    {
        Circle bigCircle = new Circle(true);
        Circle smallCircle = new Circle(false);
        int centerX;
        int centerY;
        string type = "loop";

       public Loop(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;

            bigCircle.XC = centerX;
            bigCircle.YC = centerY;
            bigCircle.st = 0;
            bigCircle.end = 360;
            bigCircle.Rad = 600;
            bigCircle.Rad2 = 500;

            smallCircle.XC = centerX;
            smallCircle.YC = centerY;
            smallCircle.st = 0;
            smallCircle.end = 360;
            smallCircle.Rad = 500;

            
        }

        public void Draw(Graphics g)
        {
            bigCircle.Drawcircle(g);
            smallCircle.Drawcircle(g);
        }

        public bool inside(int mouseX, int mouseY)
        {

            float inside = ((mouseX - centerX) * (mouseX - centerX)) + ((mouseY - centerY) * (mouseY - centerY)) - (bigCircle.Rad * bigCircle.Rad);

            if (inside > 0)
            {
                //MessageBox.Show("True");
                return true;
            }
            else
            {
                //MessageBox.Show("False");
                return false;
            }


        }

        public void updatePos(int dx, int dy)
        {
            centerX += dx;
            centerY += dy;

            bigCircle.XC += dx;
            bigCircle.YC += dy;

            smallCircle.XC += dx;
            smallCircle.YC += dy;

        }
    }
    public class LineSegment : Road
    {
        public PointF ptS, ptE;
        Pen pen = new Pen(Color.Red, 5);
        public Transformation trans = new Transformation();
        public int move = 0;
        string type = "line";

        public void Draw(Graphics g)
        {
            g.DrawLine(pen, ptS.X, ptS.Y, ptE.X, ptE.Y);
            g.FillEllipse(Brushes.Gray, ptS.X - 10, ptS.Y - 10, 20, 20);
            g.FillEllipse(Brushes.Gray, ptE.X - 10, ptE.Y - 10, 20, 20);
        }

        public bool inside(int mouseX, int mouseY)
        {
            return false;
        }

        public void updatePos(int dx, int dy)
        {

        }
    }

    public partial class Form1 : Form
    {
        Timer T = new Timer();
        Bitmap image;
        List<Road> road = new List<Road>();
        Point lastMousePos;

        int lk = -1;
        int centerX, centerY;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            T.Tick += T_Tick;
            T.Start();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(lk != -1)
            {
                int dx = e.X - lastMousePos.X;
                int dy = e.Y - lastMousePos.Y;

                road[lk].updatePos(dx, dy);
                
                lastMousePos.X = e.X;
                lastMousePos.Y = e.Y;
            }

            drawdubb(this.CreateGraphics());
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            lk = -1;
        }

        private void T_Tick(object sender, EventArgs e)
        {


            //drawdubb(this.CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(centerX + ", " + centerY);

            for (int i = 0; i < road.Count; i++)
                {
                    
                    if(road[i].inside(e.X, e.Y) == true)
                    {
                        lk = i;
                        lastMousePos.X = e.X;
                        lastMousePos.Y = e.Y;
                        break;
                        //MessageBox.Show("found");
                    }
                    else
                    {
                        lk = -1;
                        //MessageBox.Show("not found");

                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.L)
            {
                Loop loop = new Loop(centerX, centerY);
                road.Add(loop);
                this.Text = centerX + ", " + centerY;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap("wallpaper.jpg");
            centerX = this.ClientSize.Width / 2;
            centerY = this.ClientSize.Height / 2;



        }

        private void drawscene(Graphics g2)
        {
            


            for (int i = 0; i < road.Count; i++)
            {
                road[i].Draw(g2);
            }
        }

        private void drawdubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(image);
            drawscene(g2);
            g.DrawImage(image, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
        }
    }
}
