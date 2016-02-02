using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    static class VectorMath
    {
        public static float DistanceFromPointToLineSegment(Vector2 point, Vector2 lineStart, Vector2 lineEnd){
            Vector2 perpendicular = lineStart - point - Vector2.Dot(lineStart - point, Vector2.Normalize(lineEnd - lineStart)) * Vector2.Normalize(lineEnd - lineStart);
            Vector2 distanceVec;
            if (Vector2.Distance(point + perpendicular, lineStart) > Vector2.Distance(lineStart, lineEnd)
                || Vector2.Distance(point + perpendicular, lineEnd) > Vector2.Distance(lineStart, lineEnd))
            {
                if (Vector2.Distance(point + perpendicular, lineStart) < Vector2.Distance(point + perpendicular, lineEnd))
                    distanceVec = point - lineStart;
                else
                    distanceVec = point - lineEnd;
            }
            else
                distanceVec = perpendicular;
            return distanceVec.Length();
        }

        public static void ClosestPtPointSegment(Vector2 v, Vector2 lineStart, Vector2 lineEnd, out float t/*parameter of closest point for line in parametric form*/, out Vector2 p/*closest point*/){
		    Vector2 seg = lineEnd - lineStart;
		    t = Vector2.Dot(v - lineStart, seg) / Vector2.Dot(seg,seg);
		    if(t < 0.0f) t = 0.0f;
		    if(t > 1.0f) t = 1.0f;
		    p = lineStart + t * seg;
	    }
    }
}
