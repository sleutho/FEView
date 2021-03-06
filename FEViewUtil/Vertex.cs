﻿
namespace FEViewUtil
{
    public class Vertex
    {
        private int _xPixel;
        private int _yPixel;
        private Point _ptModel = new Point();
        private Point _ptView = new Point();

        public Point modelPoint
        {
            get
            {
                return _ptModel;
            }
            set
            {
                _ptModel = value;
            }
        }

        public Point viewPoint
        {
            get
            {
                return _ptView;
            }
            set
            {
                _ptView = value;
            }
        }

        public int xPixel
        {
            get
            {
                return _xPixel;
            }
            set
            {
                _xPixel = value;
            }
        }

        public int yPixel
        {
            get
            {
                return _yPixel;
            }
            set
            {
                _yPixel = value;
            }
        }
    }
}
