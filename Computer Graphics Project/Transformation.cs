using Computer_Graphics_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_Project
{
    public class Transformation
    {

        public LineSegment RotateRight(LineSegment L, float xRef, float yRef, float val)
        {
            ///////////////////
            //// translate
            //////////////////
            L.ptS.X -= xRef;
            L.ptS.Y -= yRef;
            L.ptE.X -= xRef;
            L.ptE.Y -= yRef;

            ///////////////////
            //// Rotate around origin
            //////////////////
            double xn = L.ptS.X * Math.Cos(val) - L.ptS.Y * Math.Sin(val);
            double Yn = L.ptS.X * Math.Sin(val) + L.ptS.Y * Math.Cos(val);

            L.ptS.X = (float)xn;
            L.ptS.Y = (float)Yn;

            xn = L.ptE.X * Math.Cos(val) - L.ptE.Y * Math.Sin(val);
            Yn = L.ptE.X * Math.Sin(val) + L.ptE.Y * Math.Cos(val);

            L.ptE.X = (float)xn;
            L.ptE.Y = (float)Yn;

            ///////////////////
            //// undo the translation
            //////////////////
            L.ptS.X += xRef;
            L.ptS.Y += yRef;
            L.ptE.X += xRef;
            L.ptE.Y += yRef;

            return L;
        }

        public LineSegment RotateLeft(LineSegment L, float xRef, float yRef, float val)
        {
            ///////////////////
            //// translate
            //////////////////
            L.ptS.X -= xRef;
            L.ptS.Y -= yRef;
            L.ptE.X -= xRef;
            L.ptE.Y -= yRef;

            ///////////////////
            //// Rotate around origin
            //////////////////
            double xn = L.ptS.X * Math.Cos(-val) - L.ptS.Y * Math.Sin(-val);
            double Yn = L.ptS.X * Math.Sin(-val) + L.ptS.Y * Math.Cos(-val);

            L.ptS.X = (float)xn;
            L.ptS.Y = (float)Yn;

            xn = L.ptE.X * Math.Cos(-val) - L.ptE.Y * Math.Sin(-val);
            Yn = L.ptE.X * Math.Sin(-val) + L.ptE.Y * Math.Cos(-val);

            L.ptE.X = (float)xn;
            L.ptE.Y = (float)Yn;

            ///////////////////
            //// undo the translation
            //////////////////
            L.ptS.X += xRef;
            L.ptS.Y += yRef;
            L.ptE.X += xRef;
            L.ptE.Y += yRef;

            return L;
        }
    }
}
