using System.Collections.Generic;
using System.Drawing;

namespace DKFramework
{
    class GraphVertex 
    {
        public GraphVertex CameFrom;
        public float G;
        public float H;
        public float F
        {
            get { return (G + H); } 
        }
        public PointF Cord;
        public List<GraphVertex> Neighbors = new List<GraphVertex>();


        public static bool operator ==(GraphVertex gv1, GraphVertex gv2)
        {
            if (gv1.Cord == gv2.Cord)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(GraphVertex gv1, GraphVertex gv2)
        {
            return !(gv1 == gv2);
        }
    }
}
