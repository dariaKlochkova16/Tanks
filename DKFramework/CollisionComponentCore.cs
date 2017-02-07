using System;
using System.Collections.Generic;
using System.Drawing;

namespace DKFramework
{
    public class CollisionComponentCore : CoreComponentBase
    {
        private List<GameObject> dynamicObject;
        private CellData[,] _collisionMap;
        private CellData[,] _dynamicCollisionMap;
        private bool[,] _collisionLayersMap;
        private Size sizeField;
  
        public CollisionComponentCore(Core core) : base(core)
        {
            _core = Core.Instance;
            _core.DiedGameObject += new EventHandler(Remove);
            sizeField = Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField;
            _collisionMap = NewCollisionMap();
            dynamicObject = new List<GameObject>();
            InitCollisionLayersMap();
        }

        private void InitCollisionLayersMap()
        {
            const int mapSize = 11;
            _collisionLayersMap = new bool[mapSize, mapSize];

            for (int i = 0; i < mapSize; i++)
            {
                SetValueCollisionLayerMap(i, i, false);
                for (int j = i + 1; j < mapSize; j++)
                {
                    SetValueCollisionLayerMap(i, j, true);
                }
            }

            SetValueCollisionLayerMap((int)ObjectType.Bullet, (int) ObjectType.Water, false);
            SetValueCollisionLayerMap((int)ObjectType.Bullet, (int)ObjectType.Ice, false);
            SetValueCollisionLayerMap((int)ObjectType.Player, (int)ObjectType.Ice, false);         
            SetValueCollisionLayerMap((int)ObjectType.Enemy, (int)ObjectType.Ice, false); 
        }

        private void SetValueCollisionLayerMap(int i, int j, bool value)
        {
            _collisionLayersMap[i, j] = value;
            _collisionLayersMap[j, i] = value;
        }

        public bool CrossingTest(GameObject gameObject)
        {
            PointF point;
            GameObject collisionObject;
            return CrossingTest(gameObject, out point, out collisionObject);
        }

        public bool CrossingTest(GameObject gameObject, out PointF point, out GameObject collisionObject)
        {
            CreateDynamicCollisionMap(gameObject);
            Transform transform = gameObject.GetComponent<Transform>();
            int x = Math.Abs((int)transform.X);

            float colliderX = transform.X - x + transform.Size.Width;

            while (colliderX > 0)
            {
                int y = Math.Abs((int)transform.Y);
                float colliderY = Math.Abs(transform.Y) - y + transform.Size.Height;
                while (colliderY > 0)
                {
                    if (x < sizeField.Width && y < sizeField.Height)
                    {
                        if (_collisionMap[x, y].isBusy 
                            && _collisionLayersMap[(int)_collisionMap[x, y].CellGameObject.NameType,
                                     (int)gameObject.NameType])
                        {
                            point = BackPosition(gameObject, x, -y);
                            collisionObject = _collisionMap[x,y].CellGameObject;
                            return true;
                        }
                        foreach (GameObject element in dynamicObject)
                        {
                            if (_dynamicCollisionMap[x, y].isBusy 
                                && _collisionLayersMap[(int)_dynamicCollisionMap[x, y].CellGameObject.NameType,
                                         (int)gameObject.NameType])
                            {
                                point = new PointF(transform.X, transform.Y);
                                collisionObject = _dynamicCollisionMap[x, y].CellGameObject;
                                return true;
                            }
                        }
                    }
                    y++;
                    colliderY--;
                }
                x++;
                colliderX--;
            }
            point = new PointF();
            collisionObject = new GameObject(ObjectType.None);
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

        private void CreateDynamicCollisionMap(GameObject gameObject)
        {
            _dynamicCollisionMap = NewCollisionMap();

            foreach (GameObject el in dynamicObject)
            {
                if (el.GetComponent<Collider>().CollisionLayer != gameObject.GetComponent<Collider>().CollisionLayer)
                {
                    Transform transform = el.GetComponent<Transform>();
                    int x = Math.Abs((int)transform.X);

                    float colliderX = transform.X - x + transform.Size.Width;

                    while (colliderX > 0)
                    {
                        int y = Math.Abs((int)transform.Y);
                        float colliderY = Math.Abs(transform.Y) - y + transform.Size.Height;

                        while (colliderY > 0)
                        {
                            if (x < sizeField.Width && y < sizeField.Height)
                            {
                                _dynamicCollisionMap[x, y].isBusy = true;
                                _dynamicCollisionMap[x, y].CellGameObject = el;
                            }
                            y++;
                            colliderY--;
                        }
                        x++;
                        colliderX--;
                    }
                }
            }
        }

        public void Add(GameObject gameObject)
        {
            bool isStatic = gameObject.GetComponent<Collider>().IsStatic;
            if (isStatic)
            {
                Transform transform = gameObject.GetComponent<Transform>();
                int x = (int)Math.Abs(transform.X);
                int y = (int)Math.Abs(transform.Y);

                for (int i = 0; i < transform.Size.Width; i++)
                {
                    for (int j = 0; j < transform.Size.Height; j++)
                    {
                        if ((x + i <= (_collisionMap.GetLength(0) - 1)) && (y + j <= (_collisionMap.GetLength(1) - 1)))
                        {
                            _collisionMap[x + i, y + j].isBusy = true;
                            _collisionMap[x + i, y + j].CellGameObject = gameObject;
                        }
                    }
                }
            }
            else
            {
                dynamicObject.Add(gameObject);
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
                        _collisionMap[x + i, y + j].isBusy = false;
                    }
                }
            }
        }

        public void Remove(Object sender, EventArgs e)
        {  
            GameObject gameObject = (GameObject)sender;
            Transform transform = gameObject.GetComponent<Transform>();

            if (gameObject.GetComponent<Collider>().IsStatic)
            {
                int x = Math.Abs((int)transform.X);

                float colliderX = transform.X - x + transform.Size.Width;

                while (colliderX > 0)
                {
                    int y = Math.Abs((int)transform.Y);
                    float colliderY = Math.Abs(transform.Y) - y + transform.Size.Height;
                    while (colliderY > 0)
                    {
                        if (x < sizeField.Width && y < sizeField.Height)
                        {
                            _collisionMap[x, y].isBusy = false;
                        }
                        y++;
                        colliderY--;
                    }
                    x++;
                    colliderX--;
                }
            }
            else
            {
                dynamicObject.Remove(gameObject);
            }
        }

        public CellData[,] NewCollisionMap()
        {
            var collisionMap = new CellData[sizeField.Width, sizeField.Height];

            for (int i = 0; i < sizeField.Width; i++)
            {
                for (int j = 0; j < sizeField.Height; j++)
                {
                    collisionMap[i, j] = new CellData();
                    collisionMap[i, j].isBusy = false;
                }
            }
            return collisionMap;
        }

        public override void Update(float deltaTime)
        {
            GameObject collisionObject;
            PointF point;
            for (int i = 0; i < dynamicObject.Count; i++)
            {
                if (CrossingTest(dynamicObject[i], out point, out collisionObject))
                    dynamicObject[i].SendMessage(new MessageCollision(point, collisionObject));
            }
            for (int i = 0; i < dynamicObject.Count; i++)
            {
                if (Leave(dynamicObject[i], out point))
                    dynamicObject[i].SendMessage(new MessageCollision(point, null));
            }
        }

        private bool Leave(GameObject gameObject, out PointF point)
        {
            Transform transform = gameObject.GetComponent<Transform>();
            float x = transform.X;
            float y = transform.Y;
            if (x <= sizeField.Width - transform.Size.Width && y >= -sizeField.Height + transform.Size.Height
                    && y <= 0 && x >= 0)
            {
                point = new PointF(x, y);
                return false;
            }
            point = new PointF(
            Mathem.Clamp(0, sizeField.Width - transform.Size.Width, transform.X),
            Mathem.Clamp(-sizeField.Height + transform.Size.Height, 0, transform.Y));
            return true;
        }

    }
}
