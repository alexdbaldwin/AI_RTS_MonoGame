using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    struct BoundingCircle
    {
        public BoundingCircle(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }
        public Vector2 center;
        public float radius;
    }
}
