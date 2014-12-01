using System.Collections.Generic;

namespace FEViewUtil
{
    public class TriangleFace
    {
        private int[] _vertexNumbers;

        public TriangleFace(int[] vertexNumbers)
        {
            _vertexNumbers = vertexNumbers;
        }

        public Point getModelCoord(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].modelPoint;
        }

        public Point getViewCoord(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].viewPoint;
        }

        public int getPixelX(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].xPixel;
        }

        public int getPixelY(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].xPixel;
        }
    }
}
