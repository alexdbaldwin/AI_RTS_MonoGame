using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Grid
    {
        int xDimension, yDimension;
        Tile[,] tiles;
        float tileSize = 20.0f;
        Pathfinder pathfinder;


        public Rectangle Bounds {
            get {
                return new Rectangle(0,0,(int)(xDimension*tileSize),(int)(yDimension*tileSize));
            }
        }

        public Grid() {
            using (StreamReader sr = new StreamReader("testmap.txt")) {
                List<string> lines = new List<string>();
                while (!sr.EndOfStream) {
                    lines.Add(sr.ReadLine());
                }
                xDimension = lines[0].Length;
                yDimension = lines.Count;
                tiles = new Tile[xDimension, yDimension];
                for (int i = 0; i < xDimension; i++)
                {
                    for (int j = 0; j < yDimension; j++)
                    {
                        tiles[i, j] = new Tile(i, j, new Rectangle((int)(i*tileSize),(int)(j*tileSize),(int)tileSize,(int)tileSize));
                        if (lines[j][i] == '#')
                            tiles[i, j].passable = false;
                    }
                }

            }

            //Set up neighbours for pathfinding
            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    if (i > 0 && tiles[i-1, j].passable)
                        tiles[i, j].neighbours.Add(tiles[i-1, j]);
                    if (j > 0 && tiles[i, j - 1].passable)
                        tiles[i, j].neighbours.Add(tiles[i, j - 1]);
                    if (i < xDimension - 1 && tiles[i + 1, j].passable)
                        tiles[i, j].neighbours.Add(tiles[i + 1, j]);
                    if (j < yDimension - 1 && tiles[i, j + 1].passable)
                        tiles[i, j].neighbours.Add(tiles[i, j + 1]);
                }
            }

            pathfinder = new Pathfinder(this);

        }

        public bool LineOfSight(Vector2 start, Vector2 end) {
            return LineOfSight(start.X, start.Y, end.X, end.Y);
        }

        public bool LineOfSight(float startX, float startY, float endX, float endY) {
            int x = (int)(startX / tileSize);
            int y = (int)(startY / tileSize);
            int xEnd = (int)(endX / tileSize);
            int yEnd = (int)(endY / tileSize);

            if (x == xEnd && y == yEnd)
            {
                return true;
            }

            int stepX, stepY;
            if (endX >= startX)
                stepX = 1;
            else
                stepX = -1;
            if (endY >= startY)
                stepY = 1;
            else
                stepY = -1;

            Vector2 v = new Vector2(endX - startX, endY - startY);
            v.Normalize();
            float tMaxX, tMaxY, tDeltaX, tDeltaY;
            if (Math.Abs(v.X) < 0.001f)
            {
                tMaxX = float.MaxValue;
                tDeltaX = float.MaxValue;
            }
            else
            {
                tMaxX = (((float)((x + (stepX == 1 ? 1 : 0))) * tileSize) - startX) / v.X;
                tDeltaX = (float)stepX * tileSize / v.X;
            }
            if (Math.Abs(v.Y) < 0.001f)
            {
                tMaxY = float.MaxValue;
                tDeltaY = float.MaxValue;
            }
            else
            {
                tMaxY = (((float)((y + (stepY == 1 ? 1 : 0))) * tileSize) - startY) / v.Y;
                tDeltaY = (float)stepY * ((float)tileSize) / v.Y;
            }

            do
            {
                if (tMaxX < tMaxY)
                {
                    tMaxX = tMaxX + tDeltaX;
                    x = x + stepX;
                }
                else
                {
                    tMaxY = tMaxY + tDeltaY;
                    y = y + stepY;
                }
                if (!tiles[x, y].passable)
                {
                    return false;
                }
            } while (!(x == xEnd && y == yEnd));
            return true;
        }

        public bool LineOfSight(Tile start, Tile end) {
            float startX = (start.gridX + 0.5f) * tileSize;
            float startY = (start.gridY + 0.5f) * tileSize;
            float endX = (end.gridX + 0.5f) * tileSize;
            float endY = (end.gridY + 0.5f) * tileSize;
            return LineOfSight(startX, startY, endX, endY);	        
        }


        public Path FindPath(int x1, int y1, int x2, int y2)
        {
            if (Bounds.Contains(new Point(x1, y1)) && Bounds.Contains(new Point(x2, y2)))
            {
                if (LineOfSight(x1, y1, x2, y2))
                {
                    Path p = new Path();
                    p.AddPoint(new Vector2(x1, y1));
                    p.AddPoint(new Vector2(x2, y2));
                    return p;
                }
                return FindPath(tiles[x1, y1], tiles[x2, y2]);
            }
            return null;
        }

        public Path FindPath(Tile start, Tile end)
        { 
            ResetPathfinderInfo();
            return pathfinder.FindPath(start, end);
        }

        public Path FindPath(Vector2 start, Vector2 end) {
            if (Bounds.Contains(new Point((int)start.X, (int)start.Y)) && Bounds.Contains(new Point((int)end.X, (int)end.Y)))
            {
                if (LineOfSight(start,end))
                {
                    Path p = new Path();
                    p.AddPoint(start);
                    p.AddPoint(end);
                    return p;
                }
                return FindPath(tiles[(int)(start.X / tileSize), (int)(start.Y / tileSize)], tiles[(int)(end.X / tileSize), (int)(end.Y / tileSize)]);
            }
            return null;

        }

        private void ResetPathfinderInfo() {
            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    tiles[i, j].f = int.MaxValue;
                    tiles[i, j].g = int.MaxValue;
                    tiles[i, j].cameFrom = null;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < xDimension; i++) {
                for (int j = 0; j < yDimension; j++) {
                   
                    spriteBatch.Draw(AssetManager.GetTexture("pixel"), new Rectangle((int)(i * tileSize), (int)(j * tileSize), (int)tileSize, (int)tileSize), tiles[i, j].passable ? Color.White : Color.Black);
                }
            }
            //for(int i = 0; i < testPath.PointCount(); i++)
            //    spriteBatch.Draw(AssetManager.GetTexture("pixel"), new Rectangle((int)(testPath.GetPoint(i).gridX * tileSize), (int)(testPath.GetPoint(i).gridY * tileSize), (int)tileSize, (int)tileSize), Color.Red);



            
        }

        public CollisionResponse CollisionCheck(Unit u) {
            foreach(Tile t in tiles) {
                if (t.passable)
                    continue;
                CollisionResponse cr = CollisionDetection.CollisionCheck(u.boundingCircle, t.bounds);
                if (cr.collided)
                    return cr;
            }
            CollisionResponse result = new CollisionResponse();
            result.collided = false;
            return result;
        }

        public Vector2 GetWindowCenterPos(Tile t) {
            return new Vector2((t.gridX + 0.5f) * tileSize, (t.gridY + 0.5f) * tileSize);
        }

    }
}
