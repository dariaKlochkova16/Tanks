namespace DKFramework
{
    public class DamagedReceiver : ComponentBase
    {
        public int Health { get; set; }

        public DamagedReceiver(GameObject linkGameObject) : base(linkGameObject)
        {

        }

        public void DamageCaused(int damage)
        {
            Health -= damage;

            if (Health <= 0)
                Core.Instance.Remove(LinkGameObject);
        }
    }
}
