using FEViewUtil.Box;
using FEViewUtil.TriangleFace;
using FEViewUtil.Vertex;
using System;
using System.Collections;

namespace FEViewUtil
{
    class Model : IEnumerable
    {
        private string _file;
        private string _title;
        private Box _modelBox;
        private Box _viewBox;
        private Vertex[] _vertexes;
        private TriangleFace[] _faces;

        public string file
        {
            get
            {
                return _file;
            }
        }

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
                if (position < model._faces.Length - 1)
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
            _file = file;
        }
    }
}
