using System;

namespace DKFramework
{
    public class AI : ComponentBase
    {
        private MovementController _movementController;
        private Transform _transform;

        private DateTime _pastShoot;

        public AI(GameObject linkGameObject) : base(linkGameObject)
        {
            _movementController = LinkGameObject.GetComponent<MovementController>();
            _transform = LinkGameObject.GetComponent<Transform>();
            LinkGameObject.MessageReceived += MessageReceived;
            _pastShoot = DateTime.Now;
        }

        public override void Update(float deltaTime)
        {
            _movementController.MakeMovement(1);

            TimeSpan _deltaTime = DateTime.Now - _pastShoot;
            if (_deltaTime.TotalMilliseconds > 2000)
            {
                var bullet = LinkGameObject.GetComponent<ShootComponent>().Shoot();
                bullet.GetComponent<Collider>().CollisionLayer = CollisionLayer.EnemyBullet;
                Core.Instance.Add(bullet);
                _pastShoot = DateTime.Now;
            }
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
            if(gameObject != null && gameObject.NameType != ObjectType.Base)
                     Rotate();
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
