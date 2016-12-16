using System.Drawing;

namespace DKFramework
{
    class MessageCollision: MessageBase
    {
        public GameObject GameObject
        { get; private set; }
        public  PointF Point
        { get; private set; }

        public MessageCollision(PointF point, GameObject gameObject)
        {
            GameObject = gameObject;
            Point = point;
        }
    }
}
