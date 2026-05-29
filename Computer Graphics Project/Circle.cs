using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data.SqlTypes;

namespace Computer_Graphics_Project
{

    public class Circle
    {
        public int Rad;
        public int Rad2;
        public int XC;
        public int YC;
        public float thRadian;
        public float st, end;
        Pen pen;
        public bool big;

        public Circle(bool big)
        {
            this.big = big;
            if (big == true)
            {
                pen = new Pen(Color.Black, 5);
            }
            //else
            //{
            //    pen = new Pen(Color.Black, 7);

            //}
        }


        public void Drawcircle(Graphics g)
        {
            for (float i = st; i <= end; i += 1.0f)
            {
                thRadian = (float)((i * Math.PI) / 180);
                float x = (float)(Rad * Math.Cos(thRadian));
                float y = (float)(Rad * Math.Sin(thRadian));

                x += XC;
                y += YC;

                if(big == true)
                {
                    g.FillEllipse(Brushes.Black, x-7, y-7, 15, 15);
                }
                else
                {
                    g.FillEllipse(Brushes.Black, x-3, y-3, 5, 5);
                }

                if (i % 10 == 0 && big == true)
                {
                    PointF tempst = Getnextpoint((int)i, Rad2);
                    g.DrawLine(pen, x, y, tempst.X, tempst.Y);
                }
                
            }
            
        }
        public PointF Getnextpoint(int theta, int Rad)
        {

            PointF p = new PointF();

            thRadian = (float)(theta * Math.PI / 180);

            p.X = (float)(Rad * Math.Cos(thRadian)) + XC;
            p.Y = (float)(Rad * Math.Sin(thRadian)) + YC;
            return p;
        }
    }
}
