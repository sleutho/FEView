using System;

namespace FEViewUtil
{
    public class Vector
    {
        public Vector(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public Vector(double[] arr)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public Vector(Point pt)
        {
            this._x = pt.x;
            this._y = pt.y;
            this._z = pt.z;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(
                v1.x + v2.x, 
                v1.y + v2.y, 
                v1.z + v2.z);
        }

        public static Vector operator +(Vector v, Point p)
        {
            return new Vector(
                v.x + p.x,
                v.y + p.y,
                v.z + p.z);
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return Math.Sqrt(
                v1.x * v2.x +
                v1.y * v2.y +
                v1.z * v2.z);
        }


        public double length()
        {
            return Math.Sqrt(
                this._x * this._x + 
                this._y * this._y + 
                this._z * this._z);
        }

        public void normalize()
        {
            double l = length();
            this._x /= l;
            this._y /= l;
            this._z /= l;
        }

        private double _x;
        private double _y;
        private double _z;

        public double x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public double z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }
    }
}
