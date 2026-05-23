using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class Transformation
    {

        public LineSegment Rotate(LineSegment L, float xRef, float yRef, float angleRadians)
        {
            // Translate to origin
            L.ptS.X -= xRef;
            L.ptS.Y -= yRef;
            L.ptE.X -= xRef;
            L.ptE.Y -= yRef;

            // Rotate around origin
            double xn = L.ptS.X * Math.Cos(angleRadians) - L.ptS.Y * Math.Sin(angleRadians);
            double Yn = L.ptS.X * Math.Sin(angleRadians) + L.ptS.Y * Math.Cos(angleRadians);

            L.ptS.X = (float)xn;
            L.ptS.Y = (float)Yn;

            xn = L.ptE.X * Math.Cos(angleRadians) - L.ptE.Y * Math.Sin(angleRadians);
            Yn = L.ptE.X * Math.Sin(angleRadians) + L.ptE.Y * Math.Cos(angleRadians);

            L.ptE.X = (float)xn;
            L.ptE.Y = (float)Yn;

            // Translate back
            L.ptS.X += xRef;
            L.ptS.Y += yRef;
            L.ptE.X += xRef;
            L.ptE.Y += yRef;

            return L;
        }

    }
}
