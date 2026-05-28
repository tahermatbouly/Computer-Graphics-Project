using System;

namespace Computer_Graphics_Project
{
    public class DDA
    {
        public int quarter; // 1,2,3,4
        public float centerX, centerY;
        public float radius;

        public float Xst, Yst;
        public float Xend, Yend;
        public float dy, dx, m;
        public float cx, cy;
        int speed = 10;
        int direction = 1;
        public void calc()
        {
            dy = Yend - this.Yst;
            dx = Xend - Xst;
            m = dy / dx;
            cx = Xst;
            cy = Yst;
        }
        public bool CalcNextPoint()
        {
            if (direction == 0)
            {
                float tempX = Xst;
                float tempY = Yst;
                Xst = Xend;
                Yst = Yend;
                Xend = tempX;
                Yend = tempY;
                calc(); 
                direction = 1;
                return true;
            }

            if (Math.Abs(dx) > Math.Abs(dy)) 
            {
                if (Xst < Xend) 
                {
                    cx += speed;
                    cy += m * speed;

                    if (cx >= Xend)
                    {
                        cx = Xend;
                        cy = Yend;
                        direction = 0;
                    }
                }
                else 
                {
                    cx -= speed;
                    cy -= m * speed;

                    if (cx <= Xend)
                    {
                        cx = Xend;
                        cy = Yend;
                        direction = 0;
                    }
                }
            }
            else 
            {
                if (Yst < Yend) 
                {
                    cy += speed;

                    if (m != 0)
                        cx += (1 / m) * speed;

                    if (cy >= Yend)
                    {
                        cx = Xend;
                        cy = Yend;
                        direction = 0;
                    }
                }
                else 
                {
                    cy -= speed;

                    if (m != 0)
                        cx -= (1 / m) * speed;

                    if (cy <= Yend)
                    {
                        cx = Xend;
                        cy = Yend;
                        direction = 0;
                    }
                }
            }

            return true;
        }
    }  
}
