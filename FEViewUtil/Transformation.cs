using System;

namespace FEViewUtil
{
    public class Transformation
    {
        double[,] matrix = new double[3, 3];

        public Transformation()
        {
            init();
        }

        public Transformation(double phi, View.Axis a)
        {
            init();

            switch (a)
            {
                case View.Axis.NO_AXIS:
                    break;
                case View.Axis.X_AXIS:
                    matrix[1, 1] =  Math.Cos(phi * 2 * Math.PI / 360);
                    matrix[1, 2] =  Math.Sin(phi * 2 * Math.PI / 360);
                    matrix[2, 1] = -Math.Sin(phi * 2 * Math.PI / 360);
                    matrix[2, 2] =  Math.Cos(phi * 2 * Math.PI / 360);
                    break;
                case View.Axis.Y_AXIS:
                    matrix[0, 0] =  Math.Cos(phi * 2 * Math.PI / 360);
                    matrix[2, 0] =  Math.Sin(phi * 2 * Math.PI / 360);
                    matrix[0, 2] = -Math.Sin(phi * 2 * Math.PI / 360);
                    matrix[2, 2] =  Math.Cos(phi * 2 * Math.PI / 360);
                    break;
                case View.Axis.Z_AXIS:
                    matrix[0, 0] =  Math.Cos(phi * 2 * Math.PI / 360);
                    matrix[1, 0] =  Math.Sin(phi * 2 * Math.PI / 360);
                    matrix[0, 1] = -Math.Sin(phi * 2 * Math.PI / 360);
                    matrix[1, 1] =  Math.Cos(phi * 2 * Math.PI / 360);
                    break;
                default:
                    break;
            }
        }

        public Transformation(
            double scaleX,
            double scaleY,
            double scaleZ)
        {
            init();

            matrix[0, 0] = scaleX;
            matrix[1, 1] = scaleY;
            matrix[2, 2] = scaleZ;
        }

        public static Transformation operator*(
            Transformation a, Transformation b)
        {
            Transformation transformation = new Transformation();
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    transformation.matrix[i, j] = 0;
                    for (int k = 0; k < 3; ++k)
                    {
                        transformation.matrix[i, j] += 
                            a.matrix[i, k] * b.matrix[k, j];
                    }
                }
            }
            return transformation;
        }

        public static Point operator *(
            Transformation t, Point p)
        {
            double x = p.x * t.matrix[0, 0] + p.y * t.matrix[0, 1] + p.z * t.matrix[0, 2];
            double y = p.x * t.matrix[1, 0] + p.y * t.matrix[1, 1] + p.z * t.matrix[1, 2];
            double z = p.x * t.matrix[2, 0] + p.y * t.matrix[2, 1] + p.z * t.matrix[2, 2];
            return new Point(x, y, z);
        }

        void init()
        {
            for (int i = 0; i < 3; ++i)
            {
                for ( int j = 0; j < 3; ++j )
                {
                    matrix[i,j] = ( (i == j) ? 1.0 : 0.0 );
                }
            }
        }
    }
}
