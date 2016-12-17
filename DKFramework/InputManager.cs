namespace DKFramework
{
    public class InputManager : CoreComponentBase
    {
        public InputManager(Core core) : base(core)
        {
        }

        public void Rotate(Rotation rotation)
        {
            GameObject player = _core.FindElement(ObjectType.Player);
            player.GetComponent<Transform>().Rotaton = rotation;
        }

        public void Shoot()
        {
            var bullet = _core.FindElement(ObjectType.Player).GetComponent<ShootComponent>().Shoot();
            Core.Instance.Add(bullet);
            bullet.GetComponent<Collider>().CollisionLayer = CollisionLayer.PlayerBullet;
        }

        public void MakeMovement()
        {
            _core.FindElement(ObjectType.Player).GetComponent<MovementController>().MakeMovement(1);
        }
    }
}
