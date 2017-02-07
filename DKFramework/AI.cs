using System;
using System.Collections.Generic;
using System.Drawing;

namespace DKFramework
{
    public class AI : ComponentBase
    {
        private MovementController _movementController;
        private Transform _transform;
        private Size sizeField;
        private GraphVertex[,] _field = new GraphVertex[Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField.Height, 
            Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField.Width];

        private DateTime _pastShoot;

        private Stack<GraphVertex> _path;
        public AI(GameObject linkGameObject) : base(linkGameObject)
        {
            _movementController = LinkGameObject.GetComponent<MovementController>();
            _transform = LinkGameObject.GetComponent<Transform>();
            LinkGameObject.MessageReceived += MessageReceived;
            _pastShoot = DateTime.Now;
            InitField();

          
        }

        //TODO переделать
        public void InitField()
        {
            sizeField = Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField;
            for (int i = 0; i < sizeField.Height; i++)
            {
                for (int j = 0; j < sizeField.Width; j++)
                {
                    _field[i,j] = new GraphVertex();
                }
            }

            for (int i = 0; i < sizeField.Height; i++)
            {
                for (int j = 0; j < sizeField.Width; j++)
                {
                    _field[i,j].Cord = new Point(i,j);

                    if (i - 1 > 0)
                    {
                        _field[i, j].Neighbors.Add(_field[i-1,j]);
                    }

                    if (j - 1 > 0)
                    {
                        _field[i, j].Neighbors.Add(_field[i , j - 1]);
                    }

                    if (i + 1 < sizeField.Height)
                    {
                        _field[i, j].Neighbors.Add(_field[i + 1, j]);
                    }

                    if (j + 1 < sizeField.Width)
                    {
                        _field[i, j].Neighbors.Add(_field[i, j + 1]);
                    }
                }
            }
        }

        public override void Update(float deltaTime)
        {
            if (_path == null)
            {
                GraphVertex start =
                    _field[(int) Math.Abs(_transform.X), (int) Math.Abs(_transform.Y)];

                GraphVertex goal =
                    _field[(int) Math.Abs(Core.Instance.FindElement(ObjectType.Base).GetComponent<Transform>().X),
                        (int) Math.Abs(Core.Instance.FindElement(ObjectType.Base).GetComponent<Transform>().Y)];
                _path = AStar.FindPath(start, goal);
            }



            if (Mathem.AboutEqual(_path.Peek().Cord.Y, Math.Abs(_transform.Y))
                && Mathem.AboutEqual(_path.Peek().Cord.X, Math.Abs(_transform.X)))
            {
                var lastStep = _path.Pop();
                if (_path.Count > 0)
                {  
                    if (lastStep.Cord.X < _path.Peek().Cord.X)
                    {
                        _transform.Rotaton = Rotation.Right;
                    }
                    if (lastStep.Cord.X > _path.Peek().Cord.X)
                    {
                        _transform.Rotaton = Rotation.Left;
                    }

                    if (lastStep.Cord.Y > _path.Peek().Cord.Y)
                    {
                        _transform.Rotaton = Rotation.Up;
                    }
                    if (lastStep.Cord.Y < _path.Peek().Cord.Y)
                    {
                        _transform.Rotaton = Rotation.Down;
                    }
                }
            }

            _movementController.MakeMovement(1);

            TimeSpan _deltaTime = DateTime.Now - _pastShoot;
            if (_deltaTime.TotalMilliseconds > 2000)
            {
                Shoot();
                _pastShoot = DateTime.Now;
            }
        }

        private void Shoot()
        {
            var bullet = LinkGameObject.GetComponent<ShootComponent>().Shoot();
            bullet.GetComponent<Collider>().CollisionLayer = CollisionLayer.EnemyBullet;
            Core.Instance.Add(bullet);
        }

        //TODO 
        private void MessageReceived(object sender, MessageBase message)
        {
            GameObject gameObject = new GameObject(ObjectType.None);
            if (message is MessageCollision)
            {
                MessageCollision messageCollision = (MessageCollision)message;
                gameObject = messageCollision.GameObject;
            }
            if(gameObject != null)
                     Shoot();
            if(gameObject == null)
                Rotate();
        }

        private void Rotate()
        {
            Random rand = new Random(); 
           _transform.Rotaton = (Rotation)rand.Next(0, 4);     
        }
    }
}
