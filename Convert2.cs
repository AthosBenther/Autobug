using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace autobug
{
    static class Convert2
    {
        #region ToVector
        static public Vector toVector(Grid A)
        {
            return new Vector(A.RenderTransform.Value.OffsetX, A.RenderTransform.Value.OffsetY);
        }
        static public Vector toVector(Image A)
        {
            return new Vector(A.RenderTransform.Value.OffsetX, A.RenderTransform.Value.OffsetY);
        }
        static public Vector toVector(Point A)
        {
            return new Vector(A.X, A.Y);
        }
        static public Vector toVector(System.Drawing.Point A)
        {
            return new Vector(A.X, A.Y);
        }
        #endregion

        #region toPoint
        static public Point toPoint(Grid A)
        {
            return new Point(A.RenderTransform.Value.OffsetX, A.RenderTransform.Value.OffsetY);
        }
        static public Point toPoint(System.Drawing.Point A)
        {
            return new Point(A.X, A.Y);
        }
        static public Point toPoint(Vector A)
        {
            return new Point(A.X, A.Y);
        }

        #endregion

        #region ToDrawningPoint
        static public System.Drawing.Point toDrawningPoint(Grid A)
        {
            return new System.Drawing.Point(Convert.ToInt32(A.RenderTransform.Value.OffsetX), Convert.ToInt32(A.RenderTransform.Value.OffsetY));
        }
        static public System.Drawing.Point toDrawningPoint(Point A)
        {
            return new System.Drawing.Point(Convert.ToInt32(A.X), Convert.ToInt32(A.Y));
        }
        static public System.Drawing.Point toDrawningPoint(Vector A)
        {
            return new System.Drawing.Point(Convert.ToInt32(A.X), Convert.ToInt32(A.Y));
        }
        #endregion

    }
}
