using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Pathfinder
    {

        List<Tile> openSet = new List<Tile>();
        List<Tile> closedSet = new List<Tile>();
        Grid grid;

        public Pathfinder(Grid grid) {
            this.grid = grid;
        }

        private Path ReconstructPath(Tile end) {
            List<Tile> reversePath = new List<Tile>();
            Path p = new Path();
            reversePath.Add(end);
            while (end.cameFrom != null) {
                end = end.cameFrom;
                reversePath.Add(end);
            }
            for (int i = reversePath.Count - 1; i >= 0; i--) {
                p.AddPoint(grid.GetWindowCenterPos(reversePath[i]));
            }
            return p;
        }

        public Path FindPath(Tile start, Tile end) {

            if (!start.passable) {
                Path p1 = new Path();
                p1.AddPoint(grid.GetWindowCenterPos(start));
                return p1;
            }

            openSet.Clear();
            closedSet.Clear();

            start.g = 0;
            start.f = start.g + start.DistanceHeuristic(end);

            openSet.Add(start);
            
            while (openSet.Count > 0) {
                Tile current = openSet.First();
                openSet.Remove(current);
                closedSet.Add(current);


                if (current == end)
                    return ReconstructPath(end);

                foreach (Tile t in current.neighbours) {
                    if (closedSet.Contains(t))
                    {
                        continue;
                    }

                    float tentative_g = current.g + 1;
                    Tile newParent = current;
                    if (current.cameFrom != null && grid.LineOfSight(current.cameFrom, t))
                    {
                        float cost = (float)Math.Sqrt(Math.Pow(current.cameFrom.gridX - t.gridX, 2) + Math.Pow(current.cameFrom.gridY - t.gridY, 2));
                        tentative_g = current.cameFrom.g + cost;
                        newParent = current.cameFrom;
                    }

                    
                    if (!openSet.Contains(t)) {
                        //t.cameFrom = current;
                        //t.g = tentative_g;
                        //t.f = t.g + t.ManhattanDistance(end);
                        openSet.Add(t);
                    }
                    else if (tentative_g >= t.g)
                    {
                        continue;
                    }

                    t.cameFrom = newParent;
                    t.g = tentative_g;
                    t.f = t.g + t.DistanceHeuristic(end);

                }
                openSet.Sort();
            }

            //If we get here, no path was found
            Path p2 = new Path();
            p2.AddPoint(grid.GetWindowCenterPos(start));
            return p2;
        }
    }
}
