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
        //bool inside(int mouseX, int mouseY);
        //void updatePos(int dx, int dy);

        void Draw(Graphics g);
        Rectangle getBound();

        void increase();
        void decrease();
        void shiftLeft(int x);
        void shiftRight(int x);
        void rotateLeft();
        void rotateRight();

        PointF CalcNextPoint();

        

    }

    public class Loop: Road
    {
        Circle bigCircle = new Circle(true);
        Circle smallCircle = new Circle(false);
        int centerX;
        int centerY;
        int ct = 280;
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

            
        }

        public void Draw(Graphics g)
        {
            bigCircle.Drawcircle(g);
            smallCircle.Drawcircle(g);
        }

        

        

        public Rectangle getBound()
        {
            Rectangle r = new Rectangle(bigCircle.XC - bigCircle.Rad, bigCircle.YC - bigCircle.Rad, bigCircle.Rad*2, bigCircle.Rad*2);
            return r;
            
        }

        public void increase()
        {
            if (smallCircle.YC - smallCircle.Rad > 100)
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

            PointF p = new PointF();

            float thRadian = (float)(ct * Math.PI / 180);
            if (ct + 1 > 360)
            {
                ct = 0;
            }
            else
            {
                ct+=5;
            }

            p.X = (float)(bigCircle.Rad * Math.Cos(thRadian)) + bigCircle.XC - (float)(bigCircle.XC/30);
            p.Y = bigCircle.YC - (float)(bigCircle.Rad * Math.Sin(thRadian));
            if(ct == 270)
            {
                if (Form1.moveLock + 1 < Form1.road.Count)
                {
                    Form1.moveLock++;
                }
            }
            return p;
        }

        public void shiftLeft(int x)
        {
            bigCircle.XC -= x;
            smallCircle.XC -= x;
        }

        public void shiftRight(int x)
        {
            bigCircle.XC += x;
            smallCircle.XC += x;
        }

        public void rotateLeft()
        {
            throw new NotImplementedException();
        }

        public void rotateRight()
        {
            return;
        }
    }
    public class LineSegment : Road
    {
        //public PointF ptS, ptE;
        public PointF bigPtS, bigPtE;
        public PointF smallPtS, smallPtE;
        public DDA dda = new DDA();

        Pen smallPen = new Pen(Color.Orange, 5);
        Pen bigPen = new Pen(Color.Orange, 15);

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
            //for(int i = (int)bigPtS.X; i < (int)bigPtE.X - (int)bigPtS.X; i++)
            //{

            //    if (i % 25 == 0)
            //    {
            //        //g.DrawLine(smallPen, i, bigPtS.Y, i, smallPtS.Y);
            //        g.DrawLine(smallPen, this.dda.)
            //    }
            //}
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

                //Form1.oldLastPos = Form1.lastPos;
                Form1.lastPos.X = (int)bigPtE.X;
                dda.Xend = bigPtE.X;
                dda.calc();
            }
        }

        public void decrease()
        {
            if (bigPtE.X - bigPtS.X > 50)
            {
                bigPtE.X -= 50;
                smallPtE.X -= 50;

                //Form1.oldLastPos = Form1.lastPos;
                Form1.lastPos.X = (int)bigPtE.X;
                dda.Xend = bigPtE.X;
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

        public void shiftLeft(int x)
        {
            smallPtS.X -= x;
            smallPtE.X -= x;
            bigPtS.X -= x;
            bigPtE.X -= x;

            dda.Xst = bigPtS.X;
            dda.Xend = bigPtE.X;

            float cx = dda.cx;
            float Xst = dda.Xst;
            float portion = cx ;
            if (cx - Xst != x)
            {
                portion = cx - Xst;
            }
            else
            {
                portion = 0;
            }

            dda.calc();
            dda.cx += portion;

            //for(int i = 0; i < cx - Xst; i++)
            //{
            //    dda.CalcNextPoint();
            //}
        }

        public void shiftRight(int x)
        {
            smallPtS.X += x;
            smallPtE.X += x;
            bigPtS.X += x;
            bigPtE.X += x;

            dda.Xst = bigPtS.X;
            dda.Xend = bigPtE.X;

            float cx = dda.cx;
            float Xst = dda.Xst;
            dda.calc();
            //dda.cx += cx - Xst;

            //for (int i = 0; i < cx - Xst; i++)
            //{
            //    dda.CalcNextPoint();
            //}
        }

        public void rotateLeft()
        {
            LineSegment newlineB = this.trans.RotateRight(this, this.bigPtS.X, this.bigPtS.Y, -0.1f);
            this.bigPtS = newlineB.bigPtS;
            this.bigPtE = newlineB.bigPtE;

            LineSegment newlineS = this.trans.RotateRight(this, this.smallPtS.X, this.smallPtS.Y, -0.1f);
            this.smallPtS = newlineS.smallPtS;
            this.smallPtE = newlineS.smallPtE;

            this.dda.Xst = newlineB.dda.Xst;
            this.dda.Xend = newlineB.dda.Xend;
            this.dda.Yst = newlineB.dda.Yst;
            this.dda.Yend = newlineB.dda.Yend;
            this.dda.calc();


            Form1.lastPos.X = (int)this.bigPtE.X;
            Form1.lastPos.Y = (int)this.bigPtE.Y;


        }

        public void rotateRight()
        {
            LineSegment newlineB = this.trans.RotateRight(this, this.bigPtS.X, this.bigPtS.Y, 0.1f);
            this.bigPtS = newlineB.bigPtS;
            this.bigPtE = newlineB.bigPtE;

            LineSegment newlineS = this.trans.RotateRight(this, this.smallPtS.X, this.smallPtS.Y, 0.1f);
            this.smallPtS = newlineS.smallPtS;
            this.smallPtE = newlineS.smallPtE;

            this.dda.Xst = newlineB.dda.Xst;
            this.dda.Xend = newlineB.dda.Xend;
            this.dda.Yst = newlineB.dda.Yst;
            this.dda.Yend = newlineB.dda.Yend;
            this.dda.calc();


            Form1.lastPos.X = (int)this.bigPtE.X;
            Form1.lastPos.Y = (int)this.bigPtE.Y;
        }
    }

    public class Curve : Road
    {
        public BezierCurve bigCurve = new BezierCurve(Color.Orange, true);
        public BezierCurve smallCurve = new BezierCurve(Color.Orange, false);
        float t = 0;
        Pen pen = new Pen(Color.Orange, 6);

        public Curve()
        {
            
            bigCurve.SetControlPoint(new Point(Form1.lastPos.X, Form1.lastPos.Y));
            bigCurve.SetControlPoint(new Point(Form1.lastPos.X + 300, Form1.lastPos.Y - 100));
            bigCurve.SetControlPoint(new Point(Form1.lastPos.X + 600, Form1.lastPos.Y));

            smallCurve.SetControlPoint(new Point(Form1.lastPos.X, Form1.lastPos.Y - 20));
            smallCurve.SetControlPoint(new Point(Form1.lastPos.X + 300, Form1.lastPos.Y - 120));
            smallCurve.SetControlPoint(new Point(Form1.lastPos.X + 600, Form1.lastPos.Y - 20));
        }

        public PointF CalcNextPoint()
        {
            PointF curvePoint = new PointF(0,0);
            
            if (t <= 1.0)
            {
                curvePoint = bigCurve.CalcCurvePointAtTime(t);
                t += 0.1f;
            }
            else
            {
                if (Form1.moveLock + 1 < Form1.road.Count)
                {
                    Form1.moveLock++;
                }
            }
            return curvePoint;
        }


        public void Draw(Graphics g)
        {
            this.bigCurve.DrawCurve(g);
            this.smallCurve.DrawCurve(g);
            int j = 0;
            for (float i=0; i<=1.0; i+=0.001f)
            {
                PointF smallpoint = smallCurve.CalcCurvePointAtTime(i);
                PointF bigpoint = bigCurve.CalcCurvePointAtTime(i);
                if (j % 45 == 0)
                {
                    g.DrawLine(pen, smallpoint, bigpoint);
                    

                }
                j++;
            }
            
        }

        public Rectangle getBound()
        {
            int w = smallCurve.GetPoint(2).X - smallCurve.GetPoint(0).X;
            int h = (smallCurve.GetPoint(0).Y - smallCurve.GetPoint(1).Y);
            h += h / 2;
            Rectangle r = new Rectangle(smallCurve.GetPoint(0).X, smallCurve.GetPoint(1).Y, w,h );
            return r;
        }

        public void decrease()
        {
            if (Form1.verticalCurve)
            {
                if (smallCurve.GetPoint(1).Y < Form1.h - 50)
                {
                    smallCurve.ModifyCtrlPoint(1, smallCurve.GetPoint(1).X, smallCurve.GetPoint(1).Y + 50);
                    bigCurve.ModifyCtrlPoint(1, bigCurve.GetPoint(1).X, bigCurve.GetPoint(1).Y + 50);

                }
            }
            else
            {
                int diff = smallCurve.GetPoint(2).X - smallCurve.GetPoint(0).X;
                if (diff > 200)
                {
                    int median = ((smallCurve.GetPoint(2).X - smallCurve.GetPoint(0).X) / 2) + smallCurve.GetPoint(0).X;
                    smallCurve.ModifyCtrlPoint(2, smallCurve.GetPoint(2).X - 50, smallCurve.GetPoint(2).Y);
                    smallCurve.ModifyCtrlPoint(1, median, smallCurve.GetPoint(1).Y);

                    bigCurve.ModifyCtrlPoint(2, bigCurve.GetPoint(2).X - 50, bigCurve.GetPoint(2).Y);
                    bigCurve.ModifyCtrlPoint(1, median, bigCurve.GetPoint(1).Y);

                    //Form1.oldLastPos = Form1.lastPos;
                    Form1.lastPos.X = smallCurve.GetPoint(2).X;
                }
            }
        }

        public void increase()
        {
            if (Form1.verticalCurve)
            {
                if (smallCurve.GetPoint(1).Y > 200)
                {
                    smallCurve.ModifyCtrlPoint(1, smallCurve.GetPoint(1).X, smallCurve.GetPoint(1).Y - 50);
                    bigCurve.ModifyCtrlPoint(1, bigCurve.GetPoint(1).X, bigCurve.GetPoint(1).Y - 50);

                }
            }
            else
            {
                
                if (smallCurve.GetPoint(2).X < Form1.w - 100)
                {
                    int median = ((smallCurve.GetPoint(2).X - smallCurve.GetPoint(0).X) / 2) + smallCurve.GetPoint(0).X;
                    smallCurve.ModifyCtrlPoint(2, smallCurve.GetPoint(2).X + 50, smallCurve.GetPoint(2).Y);
                    smallCurve.ModifyCtrlPoint(1, median, smallCurve.GetPoint(1).Y);

                    bigCurve.ModifyCtrlPoint(2, bigCurve.GetPoint(2).X + 50, bigCurve.GetPoint(2).Y);
                    bigCurve.ModifyCtrlPoint(1, median, bigCurve.GetPoint(1).Y);

                    //Form1.oldLastPos = Form1.lastPos;
                    Form1.lastPos.X = smallCurve.GetPoint(2).X;
                }

            }
        }

        public void shiftLeft(int x)
        {
            smallCurve.ModifyCtrlPoint(0, smallCurve.GetPoint(0).X - x, smallCurve.GetPoint(0).Y);
            smallCurve.ModifyCtrlPoint(1, smallCurve.GetPoint(1).X - x, smallCurve.GetPoint(1).Y);
            smallCurve.ModifyCtrlPoint(2, smallCurve.GetPoint(2).X - x, smallCurve.GetPoint(2).Y);

            bigCurve.ModifyCtrlPoint(0, bigCurve.GetPoint(0).X - x, bigCurve.GetPoint(0).Y);
            bigCurve.ModifyCtrlPoint(1, bigCurve.GetPoint(1).X - x, bigCurve.GetPoint(1).Y);
            bigCurve.ModifyCtrlPoint(2, bigCurve.GetPoint(2).X - x, bigCurve.GetPoint(2).Y);
        }

        public void shiftRight(int x)
        {
            smallCurve.ModifyCtrlPoint(0, smallCurve.GetPoint(0).X + x, smallCurve.GetPoint(0).Y);
            smallCurve.ModifyCtrlPoint(1, smallCurve.GetPoint(1).X + x, smallCurve.GetPoint(1).Y);
            smallCurve.ModifyCtrlPoint(2, smallCurve.GetPoint(2).X + x, smallCurve.GetPoint(2).Y);

            bigCurve.ModifyCtrlPoint(0, bigCurve.GetPoint(0).X + x, bigCurve.GetPoint(0).Y);
            bigCurve.ModifyCtrlPoint(1, bigCurve.GetPoint(1).X + x, bigCurve.GetPoint(1).Y);
            bigCurve.ModifyCtrlPoint(2, bigCurve.GetPoint(2).X + x, bigCurve.GetPoint(2).Y);
        }

        public void rotateLeft()
        {
            throw new NotImplementedException();
        }

        public void rotateRight()
        {
            throw new NotImplementedException();
        }
    }

    public partial class Form1 : Form
    {
        Timer T = new Timer();
        Bitmap image = new Bitmap("cover.png");
        Bitmap buffer;
        public static List<Road> road = new List<Road>();
        Point lastMousePos;
        Graphics g, g2;
        public static Point lastPos;
        public static Point oldLastPos;
        PointF car;
        Bitmap carImage = new Bitmap("car.png");
        Curve c;

        public static int moveLock = 0;
        int selectLock = -1;

        int centerX, centerY;
        public static int w, h;
        bool isDrag = false;
        public static bool lastPosChanged = false;
        bool moveFlag = false;
        public static bool verticalCurve = true;
        int scroll = 0;
        bool start = false;
        int scrollct = 0;
        bool dynamicScroll = false;

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
            //if (car.X > centerX)
            //{
            //    dynamicScroll = true;
            //}

                if (moveFlag == true && (road.Count > 0))
                {
                    if (car.X > centerX)
                    {
                        if (scroll < (1536 * 2) - 100)
                        {
                            scroll += 25;

                            for (int i = 0; i < road.Count; i++)
                            {
                                road[i].shiftLeft(25);
                            }

                            //oldLastPos = lastPos;
                            //lastPos.X -= 50;
                            //scrollct++;
                            car.X -= 25;
                        }
                    }
                    car = road[moveLock].CalcNextPoint();
                    car.Y -= 50;
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
            if(e.KeyCode == Keys.R)
            {
                image = new Bitmap("wallpaper.png");
                start = true;
            }

            if (e.KeyCode == Keys.P)
            {
                if(start == true)
                {
                    start = false;
                    image = new Bitmap("pause.png");
                }
                else
                {
                    start = true;
                    image = new Bitmap("wallpaper.png");
                }
            }

            if (start)
            {
                if (e.KeyCode == Keys.Left)
                {
                    if (road.Count > 0)
                    {
                        road[selectLock].rotateLeft();

                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (road.Count > 0)
                    {
                        road[selectLock].rotateRight();

                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    selectLock = -1;
                }

                if (e.KeyCode == Keys.Space)
                {
                    if (moveFlag == false)
                    {
                        scroll = 0;
                        for (int i = 0; i < scrollct; i++)
                        {
                            for (int j = 0; j < road.Count; j++)
                            {
                                road[j].shiftRight(100);
                            }
                        }
                        moveFlag = true;
                        scrollct = 0;
                    }
                }
            

                if (e.KeyCode == Keys.L)
                {
                    if (selectLock == -1)
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
                            MessageBox.Show("cannot put a loop here, add a line first");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Press Enter to confirm the size of this segment before adding a new one");
                    }

                }


                if (e.KeyCode == Keys.S)
                {
                    if (lastPos.X < 1536)
                    {
                        if (selectLock == -1)
                        {

                            LineSegment line = new LineSegment();

                            road.Add(line);
                            selectLock = road.Count - 1;
                            lastPosChanged = true;
                            this.Text = lastPos.X + ", " + lastPos.Y;

                            //oldLastPos = lastPos;
                            lastPos.X = (int)line.bigPtE.X;
                            lastPos.Y = (int)line.bigPtE.Y;

                        }
                        else
                        {
                            MessageBox.Show("Press Enter to confirm the size of this segment before adding a new one");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have reached the end");

                    }


                }

                if (e.KeyCode == Keys.C)
                {
                    if (lastPos.X < 1536)
                    {
                        if (selectLock == -1)
                        {

                            Curve curve = new Curve();

                            road.Add(curve);
                            selectLock = road.Count - 1;
                            lastPosChanged = false;
                            this.Text = lastPos.X + ", " + lastPos.Y;

                            oldLastPos = lastPos;
                            lastPos.X = curve.bigCurve.GetPoint(2).X;
                            lastPos.Y = curve.bigCurve.GetPoint(2).Y;

                        }
                        else
                        {
                            MessageBox.Show("Press Enter to confirm the size of this segment before adding a new one");
                        }
                    }


                }

                if (e.KeyCode == Keys.Up)
                {
                    if (selectLock >= 0)
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

                if (e.KeyCode == Keys.H)
                {
                    verticalCurve = false;
                }
                if (e.KeyCode == Keys.V)
                {
                    verticalCurve = true;
                }

                if (e.KeyCode == Keys.X)
                {
                    if (scroll < (1536*2) - 100)
                    {
                        scroll += 100;

                        for (int i = 0; i < road.Count; i++)
                        {
                            road[i].shiftLeft(100);
                        }

                        //oldLastPos = lastPos;
                        lastPos.X -= 100;
                        scrollct++;
                    }
                }

                if (e.KeyCode == Keys.Z)
                {
                    if (scroll > 100)
                    {
                        scroll -= 100;

                        for (int i = 0; i < road.Count; i++)
                        {
                            road[i].shiftRight(100);
                        }

                        //oldLastPos = lastPos;
                        lastPos.X += 100;
                        scrollct--;

                    }
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
            car = new PointF(0, centerY - 50);
            moveLock = 0;

            



        }

        private void drawscene(Graphics g2)
        {
            g2.Clear(Color.White);
            //croprect = new Rectangle(0, start, 1920, 1080);
            Pen pen = new Pen(Color.White);

            g2.DrawImage(image,            // source
               new Rectangle(0, 0, w, h), // where to draw in new bitmap
               new Rectangle(scroll, 0, 1536, 1024),// what part to copy from source
               GraphicsUnit.Pixel       // unit type (pixels)
             );


            //g2.FillEllipse(Brushes.Red, centerX, centerY, 200, 200);


            if(start == true)
            {
                for (int i = 0; i < road.Count; i++)
                {
                    road[i].Draw(g2);
                }

                if (selectLock >= 0)
                {
                    g2.DrawRectangle(Pens.Red, road[selectLock].getBound());
                }

                if (moveFlag == true)
                {
                    //g2.FillEllipse(Brushes.Red, car.X - 20, car.Y - 20, 30, 30);
                    g2.DrawImage(new Bitmap(carImage, 80, 80), car);
                }
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
            g.DrawImage(buffer, 0, 0);
        }
    }
}
