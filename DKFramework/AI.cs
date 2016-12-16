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
        }

        public override void Update(float deltaTime)
        {
            _movementController.MakeMovement(1);

            TimeSpan _deltaTime = DateTime.Now - _pastShoot;
            if (_deltaTime.TotalMilliseconds > 5000)
            {
                var bullet = LinkGameObject.GetComponent<ShootComponent>().Shoot();
                Core.Instance.Add(bullet);
                _pastShoot = DateTime.Now;
            }
        }

        private void MessageReceived(object sender, MessageBase message)
        {
            Rotate();
        }

        private void Rotate()
        {
            Random rand = new Random();
          
                _transform.Rotaton = (Rotation)rand.Next(0, 4);
           
        }
    }
}
