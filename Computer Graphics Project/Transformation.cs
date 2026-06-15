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
            L.bigPtS.X -= xRef;
            L.bigPtS.Y -= yRef;
            L.bigPtE.X -= xRef;
            L.bigPtE.Y -= yRef;

            L.smallPtS.X -= xRef;
            L.smallPtS.Y -= yRef;
            L.smallPtE.X -= xRef;
            L.smallPtE.Y -= yRef;

            //dda
            L.dda.Xst -= xRef;
            L.dda.Yst -= yRef;
            L.dda.Xend -= xRef;
            L.dda.Yend -= yRef;

            ///////////////////
            //// Rotate around origin
            //////////////////
            double xn = L.bigPtS.X * Math.Cos(val) - L.bigPtS.Y * Math.Sin(val);
            double Yn = L.bigPtS.X * Math.Sin(val) + L.bigPtS.Y * Math.Cos(val);

            L.bigPtS.X = (float)xn;
            L.bigPtS.Y = (float)Yn;

            xn = L.bigPtE.X * Math.Cos(val) - L.bigPtE.Y * Math.Sin(val);
            Yn = L.bigPtE.X * Math.Sin(val) + L.bigPtE.Y * Math.Cos(val);

            L.bigPtE.X = (float)xn;
            L.bigPtE.Y = (float)Yn;


            //// samll
            double xns = L.smallPtS.X * Math.Cos(val) - L.smallPtS.Y * Math.Sin(val);
            double Yns = L.smallPtS.X * Math.Sin(val) + L.smallPtS.Y * Math.Cos(val);

            L.smallPtS.X = (float)xns;
            L.smallPtS.Y = (float)Yns;

            xns = L.smallPtE.X * Math.Cos(val) - L.smallPtE.Y * Math.Sin(val);
            Yns = L.smallPtE.X * Math.Sin(val) + L.smallPtE.Y * Math.Cos(val);

            L.smallPtE.X = (float)xns;
            L.smallPtE.Y = (float)Yns;

            //dda
            double xnd = L.dda.Xst * Math.Cos(val) - L.dda.Yst * Math.Sin(val);
            double Ynd = L.dda.Xst * Math.Sin(val) + L.dda.Yst * Math.Cos(val);

            L.dda.Xst = (float)xnd;
            L.dda.Yst = (float)Ynd;

            xnd = L.dda.Xend * Math.Cos(val) - L.dda.Yend * Math.Sin(val);
            Ynd = L.dda.Xend * Math.Sin(val) + L.dda.Yend * Math.Cos(val);

            L.dda.Xend = (float)xns;
            L.dda.Yend = (float)Yns;

            ///////////////////
            //// undo the translation
            //////////////////
            L.bigPtS.X += xRef;
            L.bigPtS.Y += yRef;
            L.bigPtE.X += xRef;
            L.bigPtE.Y += yRef;

            // small
            L.smallPtS.X += xRef;
            L.smallPtS.Y += yRef;
            L.smallPtE.X += xRef;
            L.smallPtE.Y += yRef;

            //dda
            L.dda.Xst += xRef;
            L.dda.Yst += yRef;
            L.dda.Xend += xRef;
            L.dda.Yend += yRef;

            return L;
        }

        public LineSegment RotateLeft(LineSegment L, float xRef, float yRef, float val)
        {
            ///////////////////
            //// translate
            //////////////////
            L.bigPtS.X -= xRef;
            L.bigPtS.Y -= yRef;
            L.bigPtE.X -= xRef;
            L.bigPtE.Y -= yRef;

            ///////////////////
            //// Rotate around origin
            //////////////////
            double xn = L.bigPtS.X * Math.Cos(-val) - L.bigPtS.Y * Math.Sin(-val);
            double Yn = L.bigPtS.X * Math.Sin(-val) + L.bigPtS.Y * Math.Cos(-val);

            L.bigPtS.X = (float)xn;
            L.bigPtS.Y = (float)Yn;

            xn = L.bigPtE.X * Math.Cos(-val) - L.bigPtE.Y * Math.Sin(-val);
            Yn = L.bigPtE.X * Math.Sin(-val) + L.bigPtE.Y * Math.Cos(-val);

            L.bigPtE.X = (float)xn;
            L.bigPtE.Y = (float)Yn;

            ///////////////////
            //// undo the translation
            //////////////////
            L.bigPtS.X += xRef;
            L.bigPtS.Y += yRef;
            L.bigPtE.X += xRef;
            L.bigPtE.Y += yRef;

            return L;
        }
    }
}
