using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace FEViewUtil
{
    public class Model : IEnumerable
    {
        private string _title;
        private Box _modelBox = new Box();
        private Box _viewBox = new Box();
        private List<Vertex> _vertexes = new List<Vertex>();
        private List<TriangleFace> _faces = new List<TriangleFace>();

        public string title
        {
            get
            {
                return _title;
            }
        }

        public Box getModelBox()
        { 
            return _modelBox; 
        }

        public Box getViewBox()
        {
            return _viewBox;
        }

        public Vertex getVertex(int i)
        {
            return _vertexes[i];
        }

        public List<Vertex> getVertexArray()
        {
            return _vertexes;
        }

        public int getNumberOfVertexes()
        {
            return _vertexes.Count;
        }

        public FaceEnumerator GetEnumerator()
        {
            return new FaceEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new FaceEnumerator(this);
        }

        public class FaceEnumerator : IEnumerator
        {
            private int position = -1;
            private Model model;

            public FaceEnumerator(Model model)
            {
                this.model = model;
            }

            public bool MoveNext()
            {
                if (position < model._faces.Count - 1)
                {
                    ++position;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                position = -1;
            }

            public TriangleFace Current
            {
                get
                {
                    return model._faces[position];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return model._faces[position];
                }
            }

        }

        public void computeViewBox()
        {
            for (int i = 0; i < getNumberOfVertexes(); ++i)
            {
                Vertex vertex = getVertex(i);

                _viewBox.add(vertex.viewPoint);
            }
        }

        //Handle z-Buffer painting order this way 
        public void sortFaces()
        {
            _faces.Sort(new TriangleFaceSort(this));
        }

        private class TriangleFaceSort : IComparer<TriangleFace>
        {
            private Model _model;
            public TriangleFaceSort(Model model)
            {
                _model = model;
            }

            public int Compare(TriangleFace faceA, TriangleFace faceB)
            {
                if (faceA == null)
                {
                    if (faceB == null)
                    {
                        // If x is null and y is null, they're 
                        // equal.  
                        return 0;
                    }
                    else
                    {
                        // If x is null and y is not null, y 
                        // is greater.  
                        return -1;
                    }
                }
                else
                {
                    // If x is not null... 
                    // 
                    if (faceB == null)
                    // ...and y is null, x is greater.
                    {
                        return 1;
                    }
                    else
                    {
                        //find min z coordinate to paint
                        Point ap0 = faceA.getViewCoord(_model.getVertexArray(), 0);
                        Point ap1 = faceA.getViewCoord(_model.getVertexArray(), 1);
                        Point ap2 = faceA.getViewCoord(_model.getVertexArray(), 2);

                        double amaxZ = ap0.z;
                        if (ap1.z > amaxZ)
                            amaxZ = ap1.z;
                        if (ap2.z > amaxZ)
                            amaxZ = ap2.z;

                        Point bp0 = faceB.getViewCoord(_model.getVertexArray(), 0);
                        Point bp1 = faceB.getViewCoord(_model.getVertexArray(), 1);
                        Point bp2 = faceB.getViewCoord(_model.getVertexArray(), 2);

                        double bmaxZ = bp0.z;
                        if (bp1.z > bmaxZ)
                            bmaxZ = bp1.z;
                        if (bp2.z > bmaxZ)
                            bmaxZ = bp2.z;

                        if (amaxZ < bmaxZ)
                            return -1;

                        if (amaxZ == bmaxZ)
                            return 0;
                        return 1;
                    }
                }
            }
        }


        public void read(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file, System.Text.Encoding.GetEncoding("iso-8859-1"), false))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line.StartsWith("---KNOTEN"))
                        {
                            readVertexes(sr);
                        }
                        else if (line.StartsWith("---ELEMENTE-2D-V1"))
                        {
                            readFaces(sr, new int[] { 2, 3, 4});
                        }
                        else if (line.StartsWith("---ELEMENTE-2D"))
                        {
                            readFaces(sr, new int[] { 1, 2, 3});
                        }
                        else if (line.StartsWith("---TITEL"))
                        {
                            string titleLine = sr.ReadLine();
                            this._title = titleLine.Trim();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void readVertexes(StreamReader sr)
        {
            int peek = sr.Peek();
            while ( peek >= 0 )
            {
                if ((char)peek != '*')
                    break;
                sr.ReadLine();
                peek = sr.Peek();
            }

            while ((char)peek != '-')
            {
                string line = sr.ReadLine();
                string[] fields = line.Split(null as char[], 
                    System.StringSplitOptions.RemoveEmptyEntries);

                Vertex v = new Vertex();
                v.modelPoint = new Point(
                    double.Parse(fields[1]),
                    double.Parse(fields[2]),
                    double.Parse(fields[3]));
                _vertexes.Add(v);

                _modelBox.add(v.modelPoint);

                peek = sr.Peek();
            }
        }

        private void readFaces(StreamReader sr, int[] indexes)
        {
            int peek = sr.Peek();
            while (peek >= 0)
            {
                if ((char)peek != '*')
                    break;
                sr.ReadLine();
                peek = sr.Peek();
            }

            while ((char)peek != '-')
            {
                string line = sr.ReadLine();
                string[] fields = line.Split(null as char[],
                    System.StringSplitOptions.RemoveEmptyEntries);

                int[] vertexNumbers = {
                            int.Parse(fields[indexes[0]]),
                            int.Parse(fields[indexes[1]]),
                            int.Parse(fields[indexes[2]])};

                _faces.Add(new TriangleFace(vertexNumbers));

                peek = sr.Peek();
            }
        }
    }
}
