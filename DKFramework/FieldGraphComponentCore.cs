using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using SharpGL.SceneGraph.Effects;

namespace DKFramework
{
   public class FieldGraphComponentCore: CoreComponentBase
   {
       private GraphVertex[,] _graph;
        private Size _sizeField;
        private List<PointF> _blackList = new List<PointF>();

        public GraphVertex[,] Graph
        {
            get
            {
                CreateBlackList();
                CreateGraph();
                InitGraph();
                return _graph;
            }
            private set { _graph = value; }
        }

        public FieldGraphComponentCore(Core core) : base(core)
        {
            _sizeField =Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField;        
        }

        private void CreateBlackList()
        {
            for (int i = 0; i < Core.Instance.Count; i++)
            {
                var gameObject = Core.Instance.GetElement(i);
                if (gameObject.NameType == ObjectType.ConcreteWall || gameObject.NameType == ObjectType.Water)
                {
                    var point = gameObject.GetComponent<Transform>().AbsPosition;
                    _blackList.Add(point);


                    AddNeighborsInBlackList((int)point.X - 1, (int)point.Y);
                    AddNeighborsInBlackList((int)point.X, (int)point.Y - 1);
                }
            }
        }

        private void AddNeighborsInBlackList(int i, int j)
        {
            if (!BlackListTest(i, j))
            {
                _blackList.Add(new PointF(i, j));
            }
        }

        private void CreateGraph()
        {
            _graph = new GraphVertex[_sizeField.Height, _sizeField.Width];
            for (int i = 0; i < _sizeField.Height; i++)
            {
                for (int j = 0; j < _sizeField.Width; j++)
                {
                    if (!BlackListTest(i, j))
                    {
                        _graph[i, j] = new GraphVertex();
                    }
                }
            }
        }

        public void InitGraph()
        {
            for (int i = 0; i < _sizeField.Height; i++)
            {
                for (int j = 0; j < _sizeField.Width; j++)
                {
                    if (!BlackListTest(i, j))
                    {
                        _graph[i, j].Cord = new Point(i, j);
                        AddNeighbors(i, j);
                    }
                }
            }
        }

        private bool BlackListTest(int i, int j)
        {
            var point = new PointF(Math.Abs(i), Math.Abs(j));
            foreach (var el in _blackList)
            {
                if (el == point)
                    return true;
            }
            return false;
        }

        private void AddNeighbors(int i, int j)
        {
            if (i - 1 > 0 && !BlackListTest(i - 1, j))
            {
                _graph[i, j].Neighbors.Add(_graph[i - 1, j]);
            }

            if (j - 1 > 0 && !BlackListTest(i, j - 1))
            {
                _graph[i, j].Neighbors.Add(_graph[i, j - 1]);
            }

            if (i + 1 < _sizeField.Height && !BlackListTest(i + 1, j))
            {
                _graph[i, j].Neighbors.Add(_graph[i + 1, j]);
            }

            if (j + 1 < _sizeField.Width && !BlackListTest(i, j + 1))
            {
                _graph[i, j].Neighbors.Add(_graph[i, j + 1]);
            }
        }
    }
}
