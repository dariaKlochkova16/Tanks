
namespace DKFramework
{
    public class ComponentBase
    {
         public GameObject LinkGameObject
        {
            get; 
            private set;
        }

        public ComponentBase(GameObject linkGameObject)
        {
            LinkGameObject = linkGameObject;
        }

        public virtual void Update(float deltaTime) { }
    }
}
