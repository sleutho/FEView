using System;
using System.Collections.Generic;
using FEViewUtil;

namespace FEViewConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            model.read(args[0]);

            View view = new View();

            modelViewTransformation(model, view);
            points2Pixel(model, view, 500, 500);

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(500, 500);
            paint(model, view, bitmap);

            bitmap.Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }

        static void modelViewTransformation(Model model, View view)
        {
            Box modelBox = model.getModelBox();
            Point middlePoint = modelBox.getMiddlePoint();
            Vector middleVector = modelBox.getMiddleVector();

            for (int i = 0; i < model.getNumberOfVertexes(); ++i)
            {
                Vertex vertex = model.getVertex(i);
                Point point = vertex.modelPoint - middleVector;
                vertex.viewPoint = view.transformation * point + middleVector;
            }

            if (view.perspectiveProjection)
            {
                for (int i = 0; i < model.getNumberOfVertexes(); ++i)
                {
                    Vertex vertex = model.getVertex(i);
                    Point point = vertex.viewPoint - middleVector;

                    if (point.z == view.projectionCenterZ)
                    {
                        vertex.viewPoint = new Point(
                        ((view.projectionPictureZ - view.projectionCenterZ) * (point.x - view.projectionCenterX)) / (Double.Epsilon),
                        ((view.projectionPictureZ - view.projectionCenterZ) * (point.y - view.projectionCenterY)) / (Double.Epsilon),
                        point.z) + middleVector;
                    }
                    else
                    {
                        vertex.viewPoint = new Point(
                        ((view.projectionPictureZ - view.projectionCenterZ) * (point.x - view.projectionCenterX)) / (point.z - view.projectionCenterZ),
                        ((view.projectionPictureZ - view.projectionCenterZ) * (point.y - view.projectionCenterY)) / (point.z - view.projectionCenterZ),
                        point.z) + middleVector;
                    }

                }

            }

            model.computeViewBox();
        }

        static void points2Pixel(Model model, View view, int width, int height)
        {
            double midX = width / 2;
            double midY = height / 2;

            // return if there is no space for plot because of the margin 
            if (midX < view.margin || midY < view.margin)
                return;

            Box viewBox = model.getViewBox();
            Point middlePoint = viewBox.getMiddlePoint();

            //catch devide by zero, evaluate zoomfactor
            double zoomMaxY, zoomMinY, zoomMaxX, zoomMinX;
            if (Math.Abs(viewBox.maxY - middlePoint.y) < Double.Epsilon)
            {
                zoomMaxY = Math.Abs((midY - view.margin) / Double.Epsilon);
            }
            else
            {
                zoomMaxY = Math.Abs((midY - view.margin) / (viewBox.maxY - middlePoint.y));
            }

            if (Math.Abs(viewBox.minY - middlePoint.y) < Double.Epsilon)
            {
                zoomMinY = Math.Abs((midY - view.margin) / Double.Epsilon);
            }
            else
            {
                zoomMinY = Math.Abs((midY - view.margin) / (viewBox.minY - middlePoint.y));
            }

            if (Math.Abs(viewBox.maxX - middlePoint.x) < Double.Epsilon)
            {
                zoomMaxX = Math.Abs((midX - view.margin) / Double.Epsilon);
            }
            else
            {
                zoomMaxX = Math.Abs((midX - view.margin) / (viewBox.maxX - middlePoint.x));
            }

            if (Math.Abs(viewBox.minX - middlePoint.x) < Double.Epsilon)
            {
                zoomMinX = Math.Abs((midX - view.margin) / Double.Epsilon);
            }
            else
            {
                zoomMinX = Math.Abs((midX - view.margin) / (viewBox.minX - middlePoint.x));
            }

            double zoom = zoomMaxY;
            if (zoomMinY < zoom) { zoom = zoomMinY; }
            if (zoomMaxX < zoom) { zoom = zoomMaxX; }
            if (zoomMinX < zoom) { zoom = zoomMinX; }

            //zoom model, translate into the center, round and cast to int
            Vector middelVector = viewBox.getMiddleVector();
            for (int i = 0; i < model.getNumberOfVertexes(); ++i)
            {
                Vertex vertex = model.getVertex(i);

                Vector local = vertex.viewPoint - middelVector;

                vertex.xPixel = Convert.ToInt32(Math.Round(
                    local.x * zoom + middelVector.x + midX - middlePoint.x));

                vertex.yPixel = height - Convert.ToInt32(Math.Round(
                    local.y * zoom + middelVector.y + midY - middlePoint.y));
            }
        }

        static void paint(Model model, View view, System.Drawing.Bitmap bitmap)
        {
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bitmap);
            gr.Clear(System.Drawing.Color.White);

            {
                System.Drawing.Font txtFont = new System.Drawing.Font("Arial", 10);
                System.Drawing.Brush txtBrush = System.Drawing.Brushes.Black;
                System.Drawing.Point pt = new System.Drawing.Point(10, 10);
                gr.DrawString(model.title, txtFont, txtBrush, pt);
            }

            Vector vectorN = new Vector();		// Normalenvektor der Ebene (des Dreiecks)
            Vector vectorA = new Vector();		// Vektor zwischen Punkt 0 und 1 des Dreiecks
            Vector vectorB = new Vector();		// Vektor zwischen Punkt 0 und 2 des Dreiecks
            Vector vectorV = new Vector();		// Richtungsvektor des Betrachters
            Vector vectorL = new Vector();		// Richtungsvektor der Lichtquelle


            System.Drawing.Color lineColor = System.Drawing.Color.FromArgb(
                Convert.ToInt32(255 * view.alpha),
                view.lineColor.R,
                view.lineColor.G,
                view.lineColor.B);
            System.Drawing.Pen polPen = new System.Drawing.Pen(lineColor, 1.0f);

            System.Drawing.Color surfaceColor = System.Drawing.Color.FromArgb(
                Convert.ToInt32(255 * view.alpha),
                view.surfaceColor.R,
                view.surfaceColor.G,
                view.surfaceColor.B);
            System.Drawing.SolidBrush polBrush = new System.Drawing.SolidBrush(surfaceColor);


            List<Vertex> vertexArray = model.getVertexArray();

            foreach (TriangleFace face in model)
            {
                System.Drawing.Point[] pts = new System.Drawing.Point[6]
				{
					new System.Drawing.Point( face.getPixelX(vertexArray, 0),  face.getPixelY(vertexArray, 0)),
					new System.Drawing.Point( face.getPixelX(vertexArray, 1),  face.getPixelY(vertexArray, 1)),
					new System.Drawing.Point( face.getPixelX(vertexArray, 1),  face.getPixelY(vertexArray, 1)),
					new System.Drawing.Point( face.getPixelX(vertexArray, 2),  face.getPixelY(vertexArray, 2)),
					new System.Drawing.Point( face.getPixelX(vertexArray, 2),  face.getPixelY(vertexArray, 2)),
					new System.Drawing.Point( face.getPixelX(vertexArray, 0),  face.getPixelY(vertexArray, 0))
				};

                if (view.mode != View.Mode.WIREFRAME)
                {

                    if (view.mode == View.Mode.SHADOW_ON)
                    {
                        //model.GetModelBox().GetMiddleVector(&vectorV); 
                        vectorV.x = (model.getModelBox().maxX - model.getModelBox().minX) / 2;
                        vectorV.y = (model.getModelBox().maxY - model.getModelBox().minY) / 2;
                        vectorV.z = 2 * (model.getModelBox().maxZ - model.getModelBox().minZ);
                        vectorV.normalize();

                        vectorL.x = (view.lightPositionX);
                        vectorL.y = (view.lightPositionY);
                        vectorL.z = (view.lightPositionZ);
                        vectorL.normalize();

                        vectorA = new Vector(face.getViewCoord(vertexArray, 0) - face.getViewCoord(vertexArray, 1));
                        vectorB = new Vector(face.getViewCoord(vertexArray, 0) - face.getViewCoord(vertexArray, 2));
                        vectorN = vectorA % vectorB;
                        vectorN.normalize();

                        double rest = Math.Abs(view.specular *
                            Math.Pow(Math.Abs(2 * (vectorN * vectorL) + vectorV * vectorL), view.specularExponent));

                        int r = Convert.ToInt32((view.iqd * Math.Abs(vectorN * vectorL) + view.ambientR) * view.surfaceColor.R + rest * 255);
                        int g = Convert.ToInt32((view.iqd * Math.Abs(vectorN * vectorL) + view.ambientG) * view.surfaceColor.G + rest * 255);
                        int b = Convert.ToInt32((view.iqd * Math.Abs(vectorN * vectorL) + view.ambientB) * view.surfaceColor.B + rest * 255);

                        if (r > 255)
                            r = 255;
                        if (g > 255)
                            g = 255;
                        if (b > 255)
                            b = 255;

                        if (r < 0)
                            r = 0;
                        if (g < 0)
                            g = 0;
                        if (b < 0)
                            b = 0;

                        surfaceColor = System.Drawing.Color.FromArgb(
                            Convert.ToInt32(255 * view.alpha), r, g, b);
                        polBrush = new System.Drawing.SolidBrush(surfaceColor);

                    }
                    gr.FillPolygon(polBrush, pts);
                }

                if (view.mode != View.Mode.SHADOW_ON)
                {
                    gr.DrawLines(polPen, pts);
                }
            }
        }
    }
}
