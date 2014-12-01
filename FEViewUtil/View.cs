using System;

namespace FEViewUtil
{
    public class View
    {
        public enum Mode { WIREFRAME, SHADOW_ON, SHADOW_OFF };
        public enum Axis { NO_AXIS, X_AXIS, Y_AXIS, Z_AXIS };

        public Mode mode = Mode.SHADOW_OFF;

        public int margin = 10;
        public bool relativeTransformation = false;

        public Axis axis1 = Axis.NO_AXIS;
        public Axis axis2 = Axis.NO_AXIS;
        public Axis axis3 = Axis.NO_AXIS;
        public double phi1 = 0.0;
        public double phi2 = 0.0;
        public double phi3 = 0.0;

        public double scaleX = 1.0;
        public double scaleY = 1.0;
        public double scaleZ = 1.0;

        public bool perspectiveProjection = false;

        public double projectionCenterX = 0.0;
        public double projectionCenterY = 0.0;
        public double projectionCenterZ = 0.0;
        public double projectionPictureZ = 0.0;

        public double lightPositionX = 1.0;
        public double lightPositionY = 1.0;
        public double lightPositionZ = 1.0;

        public double alpha = 1.0;
        public double specular = 1.0;
        public double specularExponent = 1.0;
        public double iqd = 1.0;//light intensity

        public double ambientR = 0.0;
        public double ambientG = 0.0;
        public double ambientB = 0.0;

        public System.Drawing.Color lineColor = 
            System.Drawing.Color.FromName("Black");
        public System.Drawing.Color surfaceColor =
            System.Drawing.Color.FromName("Cyan");

        Transformation transformation = new Transformation();
    }
}
