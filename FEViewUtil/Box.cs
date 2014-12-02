
namespace FEViewUtil
{
    public class Box
    {
        private bool _empty = true;
        private double _minX;
        private double _minY;
        private double _minZ;
        private double _maxX;
        private double _maxY;
        private double _maxZ;

        public void add(Point pt)
        {
            if (_empty)
            {
                minX = maxX = pt.x;
                minY = maxY = pt.y;
                minZ = maxZ = pt.z;
                _empty = false;
            }

            if (pt.x < minX)
                minX = pt.x;
            if (pt.x > maxX)
                maxX = pt.x;

            if (pt.y < minY)
                minY = pt.y;
            if (pt.y > maxY)
                maxY = pt.y;

            if (pt.z < minZ)
                minZ = pt.z;
            if (pt.z > maxZ)
                maxZ = pt.z;
        }

        public Point getMiddlePoint()
        {
            return new Point(
                minX + (maxX - minX) / 2.0,
                minY + (maxY - minY) / 2.0,
                minZ + (maxZ - minZ) / 2.0);
        }

        public Vector getMiddleVector()
        {
            return getMiddlePoint();
        }

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
