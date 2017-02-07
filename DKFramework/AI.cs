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
        private GraphVertex[,] _graph;

        private DateTime _pastShoot;
        private DateTime _pastRotate;

        private Stack<GraphVertex> _path;
        public AI(GameObject linkGameObject) : base(linkGameObject)
        {
            _movementController = LinkGameObject.GetComponent<MovementController>();
            _transform = LinkGameObject.GetComponent<Transform>();
            LinkGameObject.MessageReceived += MessageReceived;
            _pastShoot = DateTime.Now;
            _pastRotate = DateTime.Now;
        }

        private void FindPath()
        {
            _graph = Core.Instance.GetComponent<FieldGraphComponentCore>().Graph;
            GraphVertex start =
                _graph[(int) Math.Abs(_transform.X), (int) Math.Abs(_transform.Y)];

            var bas = Core.Instance.FindElement(ObjectType.Base);
            if (bas != null)
            {
                GraphVertex goal =
                    _graph[(int) Math.Abs(bas.GetComponent<Transform>().X),
                        (int) Math.Abs(bas.GetComponent<Transform>().Y)];
                _path = AStar.FindPath(start, goal);
            }
        }


        public override void Update(float deltaTime)
        {
            if (_path == null)
            {
                FindPath();
            }

            if ( _path != null && Mathem.AboutEqual(_path.Peek().Cord.Y, Math.Abs(_transform.Y))
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
            else
            {
                TimeSpan delta = DateTime.Now - _pastRotate;
                if (delta.TotalMilliseconds > 6000)
                {
                    Rotate();
                    _pastRotate = DateTime.Now;
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

        private void MessageReceived(object sender, MessageBase message)
        {
            GameObject gameObject = new GameObject(ObjectType.None);
            if (message is MessageCollision)
            {
                MessageCollision messageCollision = (MessageCollision)message;
                gameObject = messageCollision.GameObject;
            }
            if (gameObject != null)
                Shoot();

            if (gameObject == null)
                Rotate();
        }

        private void Rotate()
        {
            Random rand = new Random();
            _transform.Rotaton = (Rotation)rand.Next(0, 4);
        }
    }
}
