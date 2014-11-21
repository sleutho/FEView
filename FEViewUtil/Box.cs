using FEViewUtil.Point;
using FEViewUtil.Vector;

namespace FEViewUtil
{
    class Box
    {
        public void add(Point pt)
        {
            if (pt.x < this.minX)
                this.minX = pt.x;
            if (pt.x > this.maxX)
                this.maxX = pt.x;

            if (pt.y < this.minY)
                this.minY = pt.y;
            if (pt.y > this.maxY)
                this.maxY = pt.y;

            if (pt.z < this.minZ)
                this.minZ = pt.z;
            if (pt.z > this.maxZ)
                this.maxZ = pt.z;
        }

        public Point getMiddlePoint()
        {
            return new Point(
                this.maxX - this.minX,
                this.maxY - this.minY,
                this.maxZ - this.minZ);
        }

        public Vector getMiddleVector()
        {
            return new Vector(getMiddlePoint());
        }

        private double _minX;
        private double _minY;
        private double _minZ;
        private double _maxX;
        private double _maxY;
        private double _maxZ;

        public double minX
        {
            get
            {
                return _minX;
            }
            set
            {
                _minX = value;
            }
        }

        public double minY
        {
            get
            {
                return _minY;
            }
            set
            {
                _minY = value;
            }
        }

        public double minZ
        {
            get
            {
                return _minZ;
            }
            set
            {
                _minZ = value;
            }
        }

        public double maxX
        {
            get
            {
                return _maxX;
            }
            set
            {
                _maxX = value;
            }
        }

        public double maxY
        {
            get
            {
                return _maxY;
            }
            set
            {
                _maxY = value;
            }
        }

        public double maxZ
        {
            get
            {
                return _maxZ;
            }
            set
            {
                _maxZ = value;
            }
        }
    }
}
