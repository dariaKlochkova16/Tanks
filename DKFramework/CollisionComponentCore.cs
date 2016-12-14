using System;
using System.Collections.Generic;
using System.Drawing;

namespace DKFramework
{
    public class CollisionComponentCore : CoreComponentBase
    {
        private List<GameObject> dynamicObject;
        private bool[,] _collisionMap;

        public Size SizeField
        {
            get; set;
        }

        public CollisionComponentCore(Core core) : base(core)
        {
            _core = Core.Instance;
            SizeField = Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField;
            newCollisionMap();
            dynamicObject = new List<GameObject>();
        }

        public bool CrossingTest(GameObject gameObject)
        {
            PointF point;
            return CrossingTest(gameObject, out point);
        }

        public bool CrossingTest(GameObject gameObject, out PointF point)
        {
            Transform transform = gameObject.GetComponent<Transform>();
            int x = Math.Abs((int)transform.X);
            
            float colliderX = transform.X - x + transform.Size.Width;

            if(colliderX != 2)
            {
                int w = 0;
                w++;
            }

            while(colliderX  > 0)
            {
                int y = Math.Abs((int)transform.Y);
                float colliderY = Math.Abs(transform.Y) - y + transform.Size.Height;
                while (colliderY  > 0)
                {
                    if (x < SizeField.Width && y < SizeField.Height)
                    {
                        if (_collisionMap[x, y] == true)
                        {
                            point = BackPosition(gameObject, x, -y);
                            return true;
                        }   
                    }
                    y++;
                    colliderY--;
                }
                x++;
                colliderX--;
            }
            point = new PointF();
            return false;
        }

        private PointF BackPosition(GameObject gameObject, int x, int y)
        {
            PointF point = new PointF();
            Transform transform = gameObject.GetComponent<Transform>();
            switch (transform.Rotaton)
            {
                case Rotation.Down:
                    point = new PointF(transform.X, y + transform.Size.Height);
                    break;
                case Rotation.Up:
                    point = new PointF(transform.X, y - transform.Size.Height + 1);
                    break;
                case Rotation.Right:
                    point = new PointF(x - transform.Size.Width, transform.Y);
                    break;
                case Rotation.Left:
                    point = new PointF(x + transform.Size.Width - 1, transform.Y);
                    break;   
            }
            return point;
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
            _collisionMap = new bool[SizeField.Width, SizeField.Height];
            for (int i = 0; i < SizeField.Width; i++)
            {
                for (int j = 0; j < SizeField.Height; j++)
                {
                    _collisionMap[i, j] = false;
                }
            }
            CreateCollisionMap();
        }

        public override void Update(float deltaTime)
        {
            PointF point;
            foreach (GameObject element in dynamicObject)
            {
                if (CrossingTest(element, out point))
                    element.SendMessage(new MessageCollision(point));
                if (Leave(element, out point))
                    element.SendMessage(new MessageCollision(point));
            }
        }

        private bool Leave(GameObject gameObject, out PointF point)
        {
            Transform transform = gameObject.GetComponent<Transform>();
            float x = transform.X;
            float y = transform.Y;
            if (x < SizeField.Width - transform.Size.Width && y > -SizeField.Height + transform.Size.Height
                    && y <= 0 && x > 0)
            {
                point = new PointF(x, y);
                return false;
            }
                point = new PointF(
                Mathem.Clamp(0, SizeField.Width - transform.Size.Width, transform.X),
                Mathem.Clamp(-SizeField.Height + transform.Size.Height, 0, transform.Y));
                return true;
        }

    }
}
