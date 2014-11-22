
namespace FEViewUtil
{
    public class TriangleFace
    {
        private int[] _vertexNumbers;

        public TriangleFace(int[] vertexNumbers)
        {
            _vertexNumbers = vertexNumbers;
        }

        Point getModelCoord(Vertex[] vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].modelPoint;
        }

        Point getViewCoord(Vertex[] vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].viewPoint;
        }

        int getPixelX(Vertex[] vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].xPixel;
        }

        int getPixelY(Vertex[] vertexes, int i)
        {
            return vertexes[_vertexNumbers[i]].xPixel;
        }
    }
}
