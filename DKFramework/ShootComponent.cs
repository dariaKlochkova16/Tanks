namespace DKFramework
{
    public class ShootComponent: ComponentBase
    {
        private GameObject _bullet;

        public ShootComponent(GameObject linkGameObject) : base(linkGameObject)
        {
        }

        public GameObject Shoot()
        {
            Transform transform = LinkGameObject.GetComponent<Transform>();
            _bullet = GameObjectFactory.CreateGameObject(ObjectType.Bullet);

            _bullet.GetComponent<Transform>().Rotaton = transform.Rotaton;
            Rotate();

            _bullet.GetComponent<MovementController>().MakeMovement(100);

            return _bullet;
        }

        private void Rotate()
        {
            Transform transform = LinkGameObject.GetComponent<Transform>();
            switch (transform.Rotaton)
            {
                case Rotation.Down:
                    _bullet.GetComponent<Transform>().X = transform.X + (float)0.5;
                    _bullet.GetComponent<Transform>().Y = transform.Y - 1;
                    break;
                case Rotation.Left:
                    _bullet.GetComponent<Transform>().X = transform.X;
                    _bullet.GetComponent<Transform>().Y = transform.Y - (float)0.5;
                    break;
                case Rotation.Right:
                    _bullet.GetComponent<Transform>().X = transform.X + 1;
                    _bullet.GetComponent<Transform>().Y = transform.Y - (float)0.5;
                    break;
                case Rotation.Up:
                    _bullet.GetComponent<Transform>().X = transform.X + (float)0.5;
                    _bullet.GetComponent<Transform>().Y = transform.Y - (float)0.2;
                    break;
            }
        }
    }
}
