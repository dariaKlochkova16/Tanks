namespace DKFramework
{
    public class Collider : ComponentBase
    {
        public bool IsStatic { get; set; }

        public CollisionLayer CollisionLayer {get; set;}

        public Collider(GameObject linkGameObject) : base(linkGameObject)
        {
           
        }

        public void Add()
        {
            Core.Instance.GetComponent<CollisionComponentCore>().Add(LinkGameObject);
        }
    }
}