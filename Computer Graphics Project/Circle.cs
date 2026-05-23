using System.Drawing;
using System;

public class Circle
{
    public int rad;
    public int xc, yc;     //This is the Center Of the Circle 
    public float st, end;  //Draw From this Start Angle To this End Angle
    public float theRadian;
    public int i;
    public void DrawCircle(Graphics g)
    {
        for (float i = st; i <= end; i += 1.0f) // walk on the angle from the start to the end and each time increase by one 
        {
            theRadian = (float)((i * Math.PI) / 180); //Convert the angle to Radian bec sin and cos work with rad not degree
            float x = (float)(rad * Math.Cos(theRadian));
            float y = (float)(rad * Math.Sin(theRadian));
            x += xc;//Because the equations work on point(0,0) after we get the point we want to put the circle in its place
            y += yc;// So we add on x += xc and y += yc
            g.FillEllipse(Brushes.Blue, x, y, 6, 6);//Draw that Pixel when all pixels drawn get the circle
        }
        PointF tempst = GetNextPoint((int)st);
        PointF tempend = GetNextPoint((int)end);
    }
    public PointF GetNextPoint(int theta)
    {
        PointF p = new PointF();
        theRadian = (float)(theta * Math.PI / 180);
        p.X = (float)(rad * Math.Cos(theRadian)) + xc;
        p.Y = (float)(rad * Math.Sin(theRadian)) + yc;
        return p;
    }
}
