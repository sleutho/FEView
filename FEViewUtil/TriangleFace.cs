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
            return vertexes[_vertexNumbers[i] - 1].modelPoint;
        }

        public Point getViewCoord(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i] - 1].viewPoint;
        }

        public int getPixelX(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i] - 1].xPixel;
        }

        public int getPixelY(List<Vertex> vertexes, int i)
        {
            return vertexes[_vertexNumbers[i] - 1].xPixel;
        }
    }
}
