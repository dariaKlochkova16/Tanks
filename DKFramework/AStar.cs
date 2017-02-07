using System;
using System.Collections.Generic;

namespace DKFramework
{
    static class AStar
    {
        public static Stack<GraphVertex> FindPath(GraphVertex start, GraphVertex goal)
        {
            List<GraphVertex> closedSet = new List<GraphVertex>();
            List<GraphVertex> openSet = new List<GraphVertex>();

            start.G = 0;
            start.H = HeuristicCostEstimate(start, goal);
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                GraphVertex x = MinF(openSet);

                if(x == goal)
                    return CreatePath(start,goal);

                openSet.Remove(x);
                closedSet.Add(x);

                foreach (var el in x.Neighbors)
                {
                    if (!closedSet.Contains(el))
                    {
                        var tentativeGscore = x.G + 1;
                        bool betterG;
                        if (openSet.Contains(el))
                        {
                            if (el.G < tentativeGscore)
                            {
                                betterG = false;
                            }
                            else
                            {
                                betterG = true;
                            }
                        }
                        else
                        {
                            openSet.Add(el);
                            betterG = true;
                        }

                        if (betterG)
                        {
                            el.CameFrom = x;
                            el.G = tentativeGscore;
                            el.H = HeuristicCostEstimate(el, goal);     
                        }
                    }
                }   
            }
            return null;
        }

        private static float HeuristicCostEstimate(GraphVertex start, GraphVertex goal)
        {
            float x = Math.Abs(start.Cord.X - goal.Cord.X);
            float y = Math.Abs(start.Cord.Y - goal.Cord.Y);

            return (float)Math.Sqrt(x * x + y * y);
        }

        private static GraphVertex MinF(List<GraphVertex> list)
        {
            float minF = list[0].F;
            GraphVertex minVertex = list[0];

            foreach (var el in list)
            {
                if (el.F < minF)
                {
                    minF = el.F;
                    minVertex = el;
                }
            }
            return minVertex;
        }

        private static Stack<GraphVertex> CreatePath(GraphVertex start, GraphVertex goal)
        {
            var el = goal;
            Stack<GraphVertex> list = new Stack<GraphVertex>();
            while (el != start)
            {
                list.Push(el);
                el = el.CameFrom;
            }
            list.Push(start);
            return list;
        }
    }
}
