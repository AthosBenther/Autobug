using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace autobug
{
    static class Measure2
    {
        #region Distances
        static public double Distance(Vector A)
        {
            return Math.Sqrt(Math.Pow(A.X, 2) + Math.Pow(A.Y, 2));
        }
        static public double Distance(Vector A, Vector B)
        {
            Vector C = Vector.Subtract(A, B);
            return Math.Sqrt(Math.Pow(C.X, 2) + Math.Pow(C.Y, 2));
        }
        static public double Distance(Grid A, Grid B)
        {
            Vector Avec = Convert2.toVector(A);
            Vector Bvec = Convert2.toVector(B);
            Vector C = Vector.Subtract(Avec, Bvec);
            return Math.Sqrt(Math.Pow(C.X, 2) + Math.Pow(C.Y, 2));
        }
        #endregion
        #region Positions
        static public Vector RelativePosition(Grid me, Vector Target)
        {
            Point TargetAsPoint = Convert2.toPoint(Target);
            return Convert2.toVector(me.PointFromScreen(TargetAsPoint));
        }

        static public Vector RelativePosition(Grid A, Grid B)
        {
            Point BPos = Convert2.toPoint(B);
            return new Vector(A.PointFromScreen(BPos).X, A.PointFromScreen(BPos).Y);
        }
        static public Vector RelativePosition(Grid me, System.Drawing.Point Target)
        {
            Point targetAsPoint = Convert2.toPoint(Target);
            return new Vector(me.PointFromScreen(targetAsPoint).X, me.PointFromScreen(targetAsPoint).Y);
        }
        #endregion
        #region Angles
        static public double AngleBetween(Vector A, Vector B)
        {
            return Math.Atan2(A.Y - B.Y, A.X - B.X);
        }
        #endregion
        #region Random
        static public int Random()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            object syncLock = new object();
            int rndResult;
            lock (syncLock)
            {
                rndResult = rnd.Next();
            }
            return rndResult;
        }
        static public int Random(int A, int B)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            object syncLock = new object();
            int rndResult;
            lock (syncLock)
            {
                rndResult = rnd.Next(A, B);
            }
            return rndResult;
        }
        static public int Random(int A)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            object syncLock = new object();
            int rndResult;
            lock (syncLock)
            {
                rndResult = rnd.Next(A);
            }
            return rndResult;
        }
        static public byte RandomByte(int A)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            object syncLock = new object();
            int rndResult;
            lock (syncLock)
            {
                rndResult = rnd.Next(A);
            }
            return Convert.ToByte(rndResult);
        }
        internal static double RandomDouble()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            object syncLock = new object();
            double rndResult;
            lock (syncLock)
            {
                rndResult = rnd.NextDouble();
            }
            return Convert.ToByte(rndResult);
        }

        #endregion
    }
}
