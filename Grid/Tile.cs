using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Tile : IComparable<Tile>
    {
        public int gridX, gridY;
        public bool passable = true;

        public List<Tile> neighbours = new List<Tile>();

        public float f = float.MaxValue;
        public float g = float.MaxValue;
        public Tile cameFrom = null;
        public Rectangle bounds;

        public Tile(int x, int y, Rectangle bounds){
            gridX = x;
            gridY = y;
            this.bounds = bounds;
        }


        public int DistanceHeuristic(Tile other) {
            //return (Math.Abs(gridX - other.gridX) + Math.Abs(gridY - other.gridY));
            return (int)Math.Sqrt(Math.Pow((gridX - other.gridX),2) + Math.Pow((gridY - other.gridY),2));
        }

        public int CompareTo(Tile other)
        {

            if (f < other.f)
                return -1;
            else if (f > other.f)
                return 1;
            else
                return 0;
        }

    }
}
