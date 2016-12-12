using System.Drawing;

namespace DKFramework
{
    public static class GameObjectFactory
    {
        public static GameObject CreateGameObject(ObjectType objectType)
        {
            var TextureCard = Core.Instance.GetComponent<ResoursMenager>().TextureCard;
            var gameObject = new GameObject(objectType.ToString());
            gameObject.AddComponent<Transform>();
            gameObject.AddComponent<Render>();
            gameObject.GetComponent<Render>().TextureFileName = "TextureCard.png";
            gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(1, 1);
            gameObject.AddComponent<AddToCollisionMap>();
        

            switch (objectType)
            {
                case ObjectType.BrickWall:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(1, 1);
                    gameObject.GetComponent<AddToCollisionMap>().Add(false);
                    break;
                case ObjectType.ConcreteWall:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(1, 1);
                    gameObject.GetComponent<AddToCollisionMap>().Add(false);
                    break;
                case ObjectType.Forest:
                    gameObject.GetComponent<AddToCollisionMap>().Add(false);
                    break;
                case ObjectType.Ice:
                    gameObject.GetComponent<AddToCollisionMap>().Add(false);
                    break;
                case ObjectType.Water:
                    gameObject.GetComponent<AddToCollisionMap>().Add(false);
                    break;
                case ObjectType.Base:
                    gameObject.GetComponent<AddToCollisionMap>().Add(false);
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(3, 3);
                    break;
                case ObjectType.Enemy:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(2, 2);
                    gameObject.AddComponent<MovementController>();
                    gameObject.GetComponent<AddToCollisionMap>().Add(true);
                    break;
                case ObjectType.Player:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(2, 2);
                    gameObject.AddComponent<MovementController>();
                    gameObject.AddComponent<ShootComponent>();
                    gameObject.GetComponent<AddToCollisionMap>().Add(true);
                    break;
                case ObjectType.Bullet:
                    gameObject.AddComponent<MovementController>();
                    gameObject.AddComponent<BulletController>();
                    gameObject.GetComponent<AddToCollisionMap>().Add(true);
                    break;
                default:
                    gameObject = null;
                    break;
            }
        
            foreach (TextureDescriptor element in TextureCard)
            {
                if (element.Name == objectType.ToString())
                {
                    gameObject.GetComponent<Render>().RectangleImage = new Rectangle(element.Point, element.Size);
                    break;
                }
            }

            return gameObject;
        }
    }
}
