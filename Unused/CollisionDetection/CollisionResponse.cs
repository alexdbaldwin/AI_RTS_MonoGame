using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    struct CollisionResponse
    {
        public bool collided;
        public Vector2 normal;
        public float penetrationDepth;
    }
}
