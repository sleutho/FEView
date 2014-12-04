using System;

namespace FEViewUtil
{
    public class Vector
    {
        private double _x;
        private double _y;
        private double _z;

        public Vector()
        {
            _x = 0.0;
            _y = 0.0;
            _z = 0.0;
        }

        public Vector(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vector(double[] arr)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vector(Point pt)
        {
            _x = pt.x;
            _y = pt.y;
            _z = pt.z;
        }

        public static implicit operator Vector(Point p)
        {
            return new Vector(p.x, p.y, p.z);
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
            return Math.Sqrt(Math.Abs(
                v1.x * v2.x +
                v1.y * v2.y +
                v1.z * v2.z));
        }

        public static Vector operator %(Vector v1, Vector v2)
        {
            return new Vector(
                v1.y * v2.z - v1.z * v2.y,
                v1.z * v2.x - v1.x * v2.z,
                v1.x * v2.y - v1.y * v2.x);
        }


        public double length()
        {
            return Math.Sqrt(
                _x * _x + 
                _y * _y + 
                _z * _z);
        }

        public void normalize()
        {
            double l = length();
            _x /= l;
            _y /= l;
            _z /= l;
        }

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
