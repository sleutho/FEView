using FEViewUtil.Vector;

namespace FEViewUtil
{
    class Point
    {
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
    }
}
