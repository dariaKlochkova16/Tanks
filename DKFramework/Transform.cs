using System;
using System.Drawing;

namespace DKFramework
{
    public class Transform : ComponentBase
    {
        private Size _size;
        private int _z;

        public Rotation Rotaton
        {
            get;
            set;
        }

        public PointF Position { get; set; }

        public PointF AbsPosition
        {
            get { return new PointF(Math.Abs(X), Math.Abs(Y));}
        }

        public float X
        {
            get { return Position.X; }
            set { Position = new PointF(value, Position.X); }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position = new PointF(Position.X, value); }
        }

        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public Transform(GameObject linkGameObject) : base(linkGameObject)
        {
            X = -10;
            Y = 10;
            Z = 0;
            Rotaton = Rotation.Up;
        }
    }
}
