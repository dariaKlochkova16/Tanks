using System;
using System.Drawing;

namespace DKFramework
{
    [Serializable]
    public class TextureDescriptor
    {
        public string Name;
        public Size Size;
        public Point Point;

        public TextureDescriptor()
        {
        }

        public TextureDescriptor(string name, int x, int y, int height, int width)
        {
            Name = name;
            Point = new Point(x, y);
            Size = new Size(width, height);
        }
    }
}
