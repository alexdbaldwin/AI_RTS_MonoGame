using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    /// <summary>
    /// Utility functions for use in collision detection
    /// </summary>
    static class CollisionDetection
    {

        //finds the closest point on a rectangle's border to a point
        static Vector2 ClosestPointPointRectangle(Vector2 p, Rectangle r){
            //float closestX = p.X;
            //closestX = Math.Max(closestX, r.Left);
            //closestX = Math.Min(closestX, r.Right);

            //float closestY = p.Y;
            //closestY = Math.Max(closestY, r.Top);
            //closestY = Math.Min(closestY, r.Bottom);

            //return new Vector2(closestX,closestY);

            float closestX;
            if (p.X <= r.Center.X)
                closestX = r.Left;
            else
                closestX = r.Right;

            float closestY;
            if (p.Y <= r.Center.Y)
                closestY = r.Top;
            else
                closestY = r.Bottom;

            return new Vector2(closestX, closestY);
        }

        public static CollisionResponse CollisionCheck(BoundingCircle c, Rectangle r) {
            CollisionResponse result = new CollisionResponse();

            Vector2 closestPoint = ClosestPointPointRectangle(c.center, r);
	
	        float distanceSquared = Vector2.DistanceSquared(closestPoint,c.center);
            bool contained = r.Contains(c.center);
	        if(distanceSquared < c.radius * c.radius || contained){
		        result.collided = true;
		        result.normal = c.center - closestPoint;
		        result.normal.Normalize();
                //Special case for a circle with center inside the rectangle
                if (contained)
                {
                    result.normal = -result.normal;
                    result.penetrationDepth = c.radius + (float)Math.Sqrt(distanceSquared);
                }
                else {
                    result.penetrationDepth = c.radius - (float)Math.Sqrt(distanceSquared);
                }
                    
	        } else {
		        result.collided = false;
	        }

            return result;
        }

        public static CollisionResponse CollisionCheck(BoundingCircle c1, BoundingCircle c2) { 
            CollisionResponse result = new CollisionResponse();

	        float distanceSquared = Vector2.DistanceSquared(c1.center, c2.center);
	        if(distanceSquared < (c1.radius + c2.radius) * (c1.radius + c2.radius)){
		        result.collided = true;
		        result.normal = c1.center - c2.center;
                //Special case for coinciding circles, set the normal to (1,0) - could be changed to random for less predictable behaviour?
                if (result.normal.Length() - 0.0001f < 0)
                    result.normal = new Vector2(1, 0);
		        result.normal.Normalize();
		        result.penetrationDepth = c1.radius + c2.radius - (float)Math.Sqrt(distanceSquared);
	        } else {
		        result.collided = false;
	        }

	        return result;
        }


    }
}
