namespace DKFramework
{
    public enum ObjectType
    {
        None,
        Water,
        ConcreteWall,
        BrickWall,
        Ice,
        Forest,
        Grid,
        Base,
        Bullet,
        Enemy,
        Player
    }

    public static class ObjectTypeTools
    {
        public static ObjectType Convert(string str)
        {
            return (ObjectType)ObjectType.Parse(typeof(ObjectType), str);
        }
    }
}
