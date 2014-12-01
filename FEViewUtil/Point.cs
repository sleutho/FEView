
namespace FEViewUtil
{
    public class Point
    {
        public Point()
        {
            this._x = 0.0;
            this._y = 0.0;
            this._z = 0.0;
        }

        public Point(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public Point(double[] arr)
        {
            this._x = x;
            this._y = y;
            this._z = z;
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

        public static Vector operator +(Point p, Vector v)
        {
            return new Vector(
                p.x + v.x,
                p.y + v.y,
                p.z + v.z);
        }

        public static Vector operator -(Point p, Vector v)
        {
            return new Vector(
                p.x - v.x,
                p.y - v.y,
                p.z - v.z);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(
                p1.x - p2.x,
                p1.y - p2.y,
                p1.z - p2.z);
        }

        public static implicit operator Point(Vector v)
        {
            return new Point(v.x, v.y, v.z);
        }
    }
}
