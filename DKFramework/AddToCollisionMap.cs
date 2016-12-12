namespace DKFramework
{
   public class AddToCollisionMap: ComponentBase
    {
        public AddToCollisionMap(GameObject linkGameObject) : base(linkGameObject)
        {
           
        }

        public void Add(bool isStatic)
        {
            Core.Instance.GetComponent<CollisionComponentCore>().Add(LinkGameObject, isStatic);
        }
    }
}
