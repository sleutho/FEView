using FEViewUtil.Box;
using FEViewUtil.TriangleFace;
using FEViewUtil.Vertex;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace FEViewUtil
{
    class Model : IEnumerable
    {
        private string _title;
        private Box _modelBox;
        private Box _viewBox;
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

        public FaceEnumerator GetEnumerator()
        {
            return new FaceEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new FaceEnumerator(this);
        }

        private class FaceEnumerator : IEnumerator
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

        //public void finalizeViewTransformation() {}

        public void read(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line == "---KNOTEN")
                        {
                            readVertexes(sr);
                        }
                        else if (line == "---ELEMENTE")
                        {
                            readFaces(sr);
                        }
                        else if (line == "---TITEL")
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
                if ( (char)peek != '*' )
                    sr.ReadLine();
                peek = sr.Peek();
            }

            while ((char)peek != '-')
            {
                string line = sr.ReadLine();
                string[] fields = line.Split(null);

                Vertex v = new Vertex();
                v.modelPoint = new Point(
                    double.Parse(fields[1]),
                    double.Parse(fields[2]),
                    double.Parse(fields[3]));
                _vertexes.Add(v);

                peek = sr.Peek();
            }
        }

        private void readFaces(StreamReader sr)
        {
            int peek = sr.Peek();
            while (peek >= 0)
            {
                if ((char)peek != '*')
                    sr.ReadLine();
                peek = sr.Peek();
            }

            while ((char)peek != '-')
            {
                string line = sr.ReadLine();
                string[] fields = line.Split(null);

                int[] vertexNumbers = {
                            int.Parse(fields[1]),
                            int.Parse(fields[1]),
                            int.Parse(fields[1])};

                _faces.Add(new TriangleFace(vertexNumbers));

                peek = sr.Peek();
            }
        }
    }
}
