using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Computer_Graphics_Project
{
    public interface Road
    {
        void Draw(Graphics g);
        bool inside(int mouseX, int mouseY);
        void updatePos(int dx, int dy);

        Rectangle getBound();

        void increase();
        void decrease();

        PointF CalcNextPoint();

        //int x {  get; }
        //int y { get; }

    }

    public class Loop: Road
    {
        Circle bigCircle = new Circle(true);
        Circle smallCircle = new Circle(false);
        int centerX;
        int centerY;
        //public int x, y;
        string type = "loop";

       public Loop(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            

            bigCircle.XC = centerX;
            bigCircle.YC = centerY;
            bigCircle.st = 0;
            bigCircle.end = 360;
            bigCircle.Rad =200;
            bigCircle.Rad2 = bigCircle.Rad - (bigCircle.Rad / 9);

            smallCircle.XC = centerX;
            smallCircle.YC = centerY;
            smallCircle.st = 0;
            smallCircle.end = 360;
            smallCircle.Rad = bigCircle.Rad - (bigCircle.Rad / 9);

            //this.x = centerX - bigCircle.Rad;
            //this.y = centerY - smallCircle.Rad;
        }

        public void Draw(Graphics g)
        {
            bigCircle.Drawcircle(g);
            smallCircle.Drawcircle(g);
        }

        public bool inside(int mouseX, int mouseY)
        {

            float inside = ((mouseX - centerX) * (mouseX - centerX)) + ((mouseY - centerY) * (mouseY - centerY)) - (bigCircle.Rad * bigCircle.Rad);

            if (inside < 0)
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

        public Rectangle getBound()
        {
            Rectangle r = new Rectangle(bigCircle.XC - bigCircle.Rad, bigCircle.YC - bigCircle.Rad, bigCircle.Rad*2, bigCircle.Rad*2);
            return r;
            
        }

        public void increase()
        {
            if (smallCircle.Rad < 300)
            {
                bigCircle.Rad += 50;
                bigCircle.Rad2 += 50;
                smallCircle.Rad += 50;

                bigCircle.YC -= 50;
                smallCircle.YC -= 50;
            }

        }

        public void decrease()
        {
            if (smallCircle.Rad > 50)
            {
                bigCircle.Rad -= 50;
                bigCircle.Rad2 -= 50;
                smallCircle.Rad -= 50;

                bigCircle.YC += 50;
                smallCircle.YC += 50;
            }

        }

        public PointF CalcNextPoint()
        {
            return new PointF(0, 0);
        }
    }
    public class LineSegment : Road
    {
        public PointF ptS, ptE;
        public PointF bigPtS, bigPtE;
        public PointF smallPtS, smallPtE;
        public DDA dda = new DDA();

        Pen smallPen = new Pen(Color.Black, 5);
        Pen bigPen = new Pen(Color.Black, 15);

        public Transformation trans = new Transformation();
        public int move = 0;
        string type = "line";

        public LineSegment()
        {
            bigPtS = new PointF(Form1.lastPos.X, Form1.lastPos.Y);
            bigPtE = new PointF(Form1.lastPos.X + 400, Form1.lastPos.Y);
            smallPtS = new PointF((float)Form1.lastPos.X, (float)Form1.lastPos.Y - 20);
            smallPtE = new PointF((float)Form1.lastPos.X + 400, (float)Form1.lastPos.Y - 20);
            dda.Xst = bigPtS.X;
            dda.Yst = bigPtS.Y;
            dda.Xend = bigPtE.X;
            dda.Yend = bigPtE.Y;
            dda.calc();
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(bigPen, bigPtS.X, bigPtS.Y, bigPtE.X, bigPtE.Y);
            g.DrawLine(smallPen, smallPtS.X, smallPtS.Y, smallPtE.X, smallPtE.Y);
            for(int i = (int)bigPtS.X; i < (int)bigPtE.X; i++)
            {
                if(i % 25 == 0)
                {
                    g.DrawLine(smallPen, i, bigPtS.Y, i, smallPtS.Y);
                }
            }
        }

        public bool inside(int mouseX, int mouseY)
        {
            if ((mouseX >= bigPtS.X && mouseX <= bigPtE.X) && (mouseY <= bigPtS.Y && mouseY >= smallPtS.Y))
            {
                //MessageBox.Show("found line");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void updatePos(int dx, int dy)
        {
            bigPtS.X += dx;
            bigPtS.Y += dy;
            bigPtE.X += dx;
            bigPtE.Y += dy;

            smallPtS.X += dx;
            smallPtS.Y += dy;
            smallPtE.X += dx;
            smallPtE.Y += dy;

        }

        public Rectangle getBound()
        {
            
            return new Rectangle((int)smallPtS.X - (this.getLength() / 20), (int)smallPtS.Y - (this.getLength() / 20), (int)(bigPtE.X - bigPtS.X) + (this.getLength() / 10), (int)(bigPtS.Y - smallPtS.Y) + (this.getLength() / 10));
        }

        public void increase()
        {
            if (bigPtE.X - bigPtS.X < 1500)
            {
                bigPtE.X += 50;

                smallPtE.X += 50;

                Form1.lastPos.X = (int)bigPtE.X;
                dda.calc();
            }
        }

        public void decrease()
        {
            if (bigPtE.X - bigPtS.X > 50)
            {
                bigPtE.X -= 50;
                smallPtE.X -= 50;

                Form1.lastPos.X = (int)bigPtE.X;
                dda.calc();

            }

        }

        private int getLength()
        {
            return (int)bigPtE.X - (int)bigPtS.X;
        }

        public PointF CalcNextPoint()
        {
            bool temp = dda.CalcNextPoint();
            if (temp == false)
            {
                if (Form1.moveLock + 1 < Form1.road.Count)
                {
                    Form1.moveLock++;
                }
            }
            return new PointF(dda.cx, dda.cy);
        }
    }

    public partial class Form1 : Form
    {
        Timer T = new Timer();
        Bitmap image = new Bitmap("wallpaper.jpg");
        Bitmap buffer;
        public static List<Road> road = new List<Road>();
        Point lastMousePos;
        Graphics g, g2;
        public static Point lastPos;
        PointF ball;

        public static int moveLock = 0;
        int selectLock = -1;

        int centerX, centerY, w, h;
        bool isDrag = false;
        public static bool lastPosChanged = false;
        bool moveFlag = false;

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
            //if (moveLock >= 0)
            //{
            //    int dx = e.X - lastMousePos.X;
            //    int dy = e.Y - lastMousePos.Y;

            //    road[moveLock].updatePos(dx, dy);

            //    lastMousePos.X = e.X;
            //    lastMousePos.Y = e.Y;
            //}

            //drawdubb(this.CreateGraphics());
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //moveLock = -1;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if(moveFlag == true && (road.Count > 0))
            {
                ball = road[moveLock].CalcNextPoint();
            }

            drawdubb(this.CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ////MessageBox.Show(e.X + ", " + e.Y);

            //for (int i = 0; i < road.Count; i++)
            //{

            //    if (road[i].inside(e.X, e.Y) == true)
            //    {
            //        moveLock = i;
            //        selectLock = i;
            //        lastMousePos.X = e.X;
            //        lastMousePos.Y = e.Y;
            //        //MessageBox.Show("found");
            //        break;
            //    }
            //    else
            //    {
            //        moveLock = -1;
            //        selectLock = -1;
            //        //MessageBox.Show("not found");

            //    }
            //}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectLock = -1;
            }

            if (e.KeyCode == Keys.Space)
            {
                moveFlag = true;
            }

            if (e.KeyCode == Keys.L)
            {
                if(selectLock == -1)
                {
                    if (lastPosChanged == true)
                    {
                        Loop loop = new Loop(lastPos.X, lastPos.Y - 200);
                        road.Add(loop);
                        selectLock = road.Count - 1;
                        lastPosChanged = false;
                        this.Text = lastPos.X + ", " + lastPos.Y;
                    }
                    else
                    {
                        MessageBox.Show("cannot put a loop here, add a line or curve");
                    }
                }
                else
                {
                    MessageBox.Show("Press Enter to confirm the size of this segment before adding a new one");
                }

            }
            

            if (e.KeyCode == Keys.S)
            {
                if (selectLock == -1)
                {

                    LineSegment line = new LineSegment();
                    //line.bigPtS = new PointF(lastPos.X, lastPos.Y);
                    //line.bigPtE = new PointF(lastPos.X + 400, lastPos.Y);
                    //line.smallPtS = new PointF((float)lastPos.X, (float)lastPos.Y - 20);
                    //line.smallPtE = new PointF((float)lastPos.X + 400, (float)lastPos.Y - 20);
                    //line.dda.Xst = line.bigPtS.X;
                    //line.dda.Yst = line.bigPtS.Y;
                    //line.dda.Xend = line.bigPtE.X;
                    //line.dda.Yend = line.bigPtE.Y;
                    //line.dda.calc();
                    road.Add(line);
                    selectLock = road.Count - 1;
                    lastPosChanged = true;
                    this.Text = lastPos.X + ", " + lastPos.Y;
                    lastPos.X = (int)line.bigPtE.X;
                    lastPos.Y = (int)line.bigPtE.Y;

                }
                else
                {
                    MessageBox.Show("Press Enter to confirm the size of this segment before adding a new one");
                }

                
            }
            
            if (e.KeyCode == Keys.Up)
            {
                if(selectLock >= 0)
                {
                    road[selectLock].increase();
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (selectLock >= 0)
                {
                    road[selectLock].decrease();
                }
            }

            drawdubb(this.CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //image = new Bitmap("wallpaper.jpg");
            //centerX = this.ClientSize.Width / 2;
            //centerY = this.ClientSize.Height / 2;

            w = this.Width;
            h = this.Height;
            centerX = w / 2;
            centerY = h - (h / 4);
            lastPos = new Point(0, centerY);
            ball = new PointF(0, centerY);
            moveLock = 0;




        }

        private void drawscene(Graphics g2)
        {
            g2.Clear(Color.White);
            //croprect = new Rectangle(0, start, 1920, 1080);
            Pen pen = new Pen(Color.White);

            g2.DrawImage(image,            // source
               new Rectangle(0, 0, w, h), // where to draw in new bitmap
               new Rectangle(0, 0, image.Width, image.Height),// what part to copy from source
               GraphicsUnit.Pixel       // unit type (pixels)
             );


            //g2.FillEllipse(Brushes.Red, centerX, centerY, 200, 200);


            for (int i = 0; i < road.Count; i++)
            {
                road[i].Draw(g2);
            }

            if(selectLock >= 0)
            {
                g2.DrawRectangle(Pens.Red, road[selectLock].getBound());
            }

            if (moveFlag == true)
            {
                g2.FillEllipse(Brushes.Red, ball.X, ball.Y, 30, 30);
            }

        }

        private void drawdubb(Graphics g)
        {
            if (buffer != null)
            {
                buffer.Dispose();
            }

            if (g2 != null)
            {
                g2.Dispose();
            }


            buffer = new Bitmap(w, h);
            g2 = Graphics.FromImage(buffer);
            drawscene(g2);
            g.DrawImage(buffer, 0, 0, w, h);
        }
    }
}
