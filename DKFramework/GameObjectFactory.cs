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
            gameObject.AddComponent<DamagedReceiver>();
            gameObject.GetComponent<DamagedReceiver>().Health = 1;
 
            switch (objectType)
            {
                case ObjectType.BrickWall:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(1, 1);
                    gameObject.AddComponent<Collider>();
                    gameObject.GetComponent<Collider>().IsStatic = true;
                    break;
                case ObjectType.ConcreteWall:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(1, 1);
                    gameObject.AddComponent<Collider>();
                    gameObject.GetComponent<Collider>().IsStatic = true;
                    break;
                case ObjectType.Forest:  
                    break;
                case ObjectType.Ice:
                    break;
                case ObjectType.Water:
                    break;
                case ObjectType.Base:
                    gameObject.AddComponent<Collider>();
                    gameObject.GetComponent<Collider>().IsStatic = true;
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(3, 3);
                    break;
                case ObjectType.Enemy:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(2, 2);
                    gameObject.AddComponent<MovementController>();
                    gameObject.AddComponent<Collider>();
                    gameObject.GetComponent<Collider>().IsStatic = false;
                    gameObject.AddComponent<AI>();
                    gameObject.AddComponent<ShootComponent>();
                    gameObject.GetComponent<Collider>().CollisionLayer = CollisionLayer.EnemyBullet;
                    break;
                case ObjectType.Player:
                    gameObject.GetComponent<Transform>().Size = new System.Drawing.Size(2, 2);
                    gameObject.AddComponent<MovementController>();
                    gameObject.AddComponent<ShootComponent>();
                    gameObject.AddComponent<Collider>();
                    gameObject.GetComponent<Collider>().IsStatic = false;
                    gameObject.GetComponent<Collider>().CollisionLayer = CollisionLayer.PlayerBullet;
                    gameObject.GetComponent<MovementController>().Velocity = 8;
                    break;
                case ObjectType.Bullet:
                    gameObject.AddComponent<MovementController>();
                    gameObject.AddComponent<BulletController>();
                    gameObject.AddComponent<Collider>();
                    gameObject.GetComponent<Collider>().IsStatic = false;
                    gameObject.GetComponent<MovementController>().Velocity = 10;
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
