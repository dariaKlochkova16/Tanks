using System;
using System.Collections.Generic;
using System.Drawing;

namespace DKFramework
{
    public class CollisionComponentCore : CoreComponentBase
    {
        private List<GameObject> dynamicObject;
        private bool[,] _collisionMap;

        public Size Size
        {
            get; set;
        }

        public CollisionComponentCore(Core core) : base(core)
        {
            _core = Core.Instance;
            Size = Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField;
            newCollisionMap();
            dynamicObject = new List<GameObject>();
        }

        public bool CrossingTest(GameObject gameObject)
        {
            float x = gameObject.GetComponent<Transform>().X;
            float y = Math.Abs(gameObject.GetComponent<Transform>().Y);

            for (int i = 0; i < gameObject.GetComponent<Transform>().Size.Width; i++)
            {
                for (int j = 0; j < gameObject.GetComponent<Transform>().Size.Height; j++)
                {
                    if ((x + i <= (_collisionMap.GetLength(0) - 1)) && (x + i > 0 || x + i == 0) && (y + j <= (_collisionMap.GetLength(1) - 1)) && (y + j > 0 || y + j == 0))
                    {
                        if (_collisionMap[(int)x + i, (int)y + j])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CreateCollisionMap()
        {
            for (int k = 0; k < _core.Count; k++)
            {
                float x = _core.GetElenent(k).GetComponent<Transform>().X;
                float y = Math.Abs(_core.GetElenent(k).GetComponent<Transform>().Y);
                for (int i = 0; i < _core.GetElenent(k).GetComponent<Transform>().Size.Width; i++)
                {
                    for (int j = 0; j < _core.GetElenent(k).GetComponent<Transform>().Size.Height; j++)
                    {
                        x += i;
                        y += j;

                        if ((x <= (_collisionMap.GetLength(0) - 1)) && (x > 0 || x == 0) && (y <= (_collisionMap.GetLength(1) - 1)) && (y > 0 || y == 0))
                        {
                            _collisionMap[(int)x, (int)y] = true;
                        }
                    }
                }
            }
        }

        public void Add(GameObject gameObject, bool isStatic)
        {
            if (isStatic)
                dynamicObject.Add(gameObject);
            else
            {
                Transform transform = gameObject.GetComponent<Transform>();
                int x = (int)Math.Abs(transform.X);
                int y = (int)Math.Abs(transform.Y);

                for (int i = 0; i < transform.Size.Width; i++)
                {
                    for (int j = 0; j < transform.Size.Height; j++)
                    {
                        if ((x <= (_collisionMap.GetLength(0) - 1)) && (y <= (_collisionMap.GetLength(1) - 1)))
                        {
                            _collisionMap[x + i, y + j] = true;
                        }
                    }
                }
            }
        }

        public void Remove(int x, int y, Size size)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    if ((x <= (_collisionMap.GetLength(0) - 1)) && (y <= (_collisionMap.GetLength(1) - 1)))
                    {
                        _collisionMap[x + i, y + j] = false;
                    }
                }
            }
        }

        public void newCollisionMap()
        {
            _collisionMap = new bool[Size.Width, Size.Height];
            for (int i = 0; i < Size.Width; i++)
            {
                for (int j = 0; j < Size.Height; j++)
                {
                    _collisionMap[i, j] = false;
                }
            }
            CreateCollisionMap();
        }

        public override void Update(float deltaTime)
        {
            foreach(GameObject element in dynamicObject)
            {
                if (CrossingTest(element) || Leave(element))
                    element.SendMessage(new MessageCollision());
            }
        }

        private bool Leave(GameObject gameObject)
        {
            float x = gameObject.GetComponent<Transform>().X;
            float y = gameObject.GetComponent<Transform>().Y;
            if (x < Size.Width - 1 && y > -Size.Height + 2 && y <= 0 && x > 0)
                return false;
            return true;
        }

    }
}
