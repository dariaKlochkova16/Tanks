using System.Drawing;

namespace DKFramework
{
    public static class GameObjectFactory
    {
        private static GameObject gameObject;

        public static GameObject CreateGameObject(ObjectType objectType)
        {
            gameObject = new GameObject(objectType);
            AddRender();
           

            switch (objectType)
            {
                case ObjectType.BrickWall:
                    AddTransform(new Size(1, 1), 0);
                    AddCollider(true);
                    AddDamaged(1);
                    break;
                case ObjectType.ConcreteWall:
                    AddTransform(new Size(1, 1), 0);
                    AddCollider(true);
                    break;
                case ObjectType.Forest:
                    AddTransform(new Size(1, 1), 1);
                    break;
                case ObjectType.Ice:
                    AddTransform(new Size(1, 1), -1);
                    break;
                case ObjectType.Water:
                    AddTransform(new Size(1, 1), -1);
                    AddCollider(true);
                    break;
                case ObjectType.Base:
                    AddCollider(true);
                    AddTransform(new Size(3, 3), 0);
                    AddDamaged(1);
                    break;
                case ObjectType.Enemy:
                    InitPlayer(CollisionLayer.EnemyBullet, 5);
                    gameObject.AddComponent<AI>();
                    break;
                case ObjectType.Player:
                    InitPlayer(CollisionLayer.PlayerBullet, 8);
                    break;
                case ObjectType.Bullet:
                    AddTransform(new Size(1, 1), 0);
                    AddMovementController(10);
                    gameObject.AddComponent<BulletController>();
                    AddCollider(false);
                    AddDamaged(1);
                    break;
                default:
                    gameObject = null;
                    break;
            }
            return gameObject;
        }

        private static void AddRender()
        {
            gameObject.AddComponent<Render>();
            gameObject.GetComponent<Render>().TextureFileName = "TextureCard.png";

            var TextureCard = Core.Instance.GetComponent<ResoursMenager>().TextureCard;
            foreach (TextureDescriptor element in TextureCard)
            {
                if (element.Name == gameObject.NameType.ToString())
                {
                    gameObject.GetComponent<Render>().RectangleImage = new Rectangle(element.Point, element.Size);
                    break;
                }
            }
        }

        private static void InitPlayer(CollisionLayer layer, int velocity)
        {
            AddTransform(new Size(2, 2), 0);
            gameObject.AddComponent<ShootComponent>();
            AddCollider(false, layer);
            AddMovementController(velocity);
            AddDamaged(1);
        }

        private static void AddTransform(Size size, int z)
        {
            gameObject.AddComponent<Transform>();
            gameObject.GetComponent<Transform>().Size = size;
            gameObject.GetComponent<Transform>().Z = z;
        }

        private static void AddMovementController(int velocity)
        {
            gameObject.AddComponent<MovementController>();
            gameObject.GetComponent<MovementController>().Velocity = velocity;
        }

        private static void AddCollider(bool isStatic)
        {
            gameObject.AddComponent<Collider>();
            gameObject.GetComponent<Collider>().IsStatic = isStatic;
        }

        private static void AddCollider(bool isStatic, CollisionLayer layer)
        {
            AddCollider(isStatic);
            gameObject.GetComponent<Collider>().CollisionLayer = layer;
        }

        private static void AddDamaged(int health)
        {
            gameObject.AddComponent<DamagedReceiver>();
            gameObject.GetComponent<DamagedReceiver>().Health = health;
        }


    }
}
