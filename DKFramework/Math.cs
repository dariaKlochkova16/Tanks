using System.Drawing;

namespace DKFramework
{
    public static class Mathem
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static PointF Lerp(PointF a, PointF b, float t)
        {
            return new PointF(
                Lerp(a.X, b.X, t),
                Lerp(a.Y, b.Y, t));
        }

        public static float Clamp(float min, float max, float value)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }
    }
}
