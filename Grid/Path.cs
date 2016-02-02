using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Path
    {
        List<Vector2> points = new List<Vector2>();

        public Path() { 
        
        }

        public int PointCount() {
            return points.Count;
        }

        public void AddPoint(Vector2 v) {
            points.Add(v);
        }

        public Vector2 GetPoint(int p) {
            return points[p];
        }

        /// <summary>
        /// Returns the position of the point t units along the path.
        /// </summary>
        /// <param name="t">Distance along the path</param>
        public Vector2 GetPosition(float t)
        {
            if (t < 0)
                return points[0];

            float pathLength = 0;
            for (int i = 1; i < points.Count; i++)
            {
                float segmentLength = Vector2.Distance(points[i - 1], points[i]);
                if (pathLength + segmentLength > t)
                {
                    return points[i - 1] + (points[i] - points[i - 1]) * ((t - pathLength) / segmentLength);
                }
                else
                {
                    pathLength += segmentLength;
                }
            }

            return points[points.Count - 1];
        }



        public float GetLength()
        {
            throw new NotImplementedException();
        }

        public float GetParam(Vector2 pos, float prevParam){
	        float pathLength = 0;
	        for(int i = 1; i < points.Count; i++){
		        float segmentLength = Vector2.Distance(points[i-1], points[i]);
		        if(pathLength + segmentLength > prevParam){
			        //We've found the segment containing the previous param, now iterate through the forthcoming segments calculating their distance from pos
			        //and stop once we find a segment that is further away than its predecessor. That predecessor is the closest segment, so the new param is the param
			        //of the point on that segment closest to pos
			        float minDistance = float.MaxValue;
			        Vector2 closestPoint;
			        float t = 0;
			        while(true){
				        float dist = VectorMath.DistanceFromPointToLineSegment(pos,points[i-1],points[i]);
                        if (float.IsNaN(dist))
                            return 0;
				        if(dist < minDistance){
					        minDistance = dist;
					        VectorMath.ClosestPtPointSegment(pos, points[i-1], points[i],out t,out closestPoint);
					        segmentLength = Vector2.Distance(points[i-1], points[i]);
					        pathLength += segmentLength;
					        i++;
				        } else {
					        //since we've overshot the goal segment by one, take off the extra amount and return the new param
					        pathLength -= Vector2.Distance(points[i-2], points[i-1]) * (1.0f - t);
					        return pathLength;
				        }
				        if(i == points.Count){
					        //the nearest segment must have been the last one
					        //since we've overshot the goal segment by one, take off the extra amount and return the new param
					        pathLength -= Vector2.Distance(points[i-2], points[i-1]) * (1.0f - t);
					        return pathLength;
				        }
			        }
			        //break;
		        } else {
			        pathLength += segmentLength;
		        }
	        }
	        return pathLength;
        }

    }
}
