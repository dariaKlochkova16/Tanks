using System.Drawing;

namespace DKFramework
{
    class MessageCollision: MessageBase
    {
        public  PointF Point
        { get; private set; }

        public MessageCollision(PointF point)
        {
            Point = point;
        }
    }
}
