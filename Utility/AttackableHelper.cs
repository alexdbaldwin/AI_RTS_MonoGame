using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    static class AttackableHelper
    {
        public static float Distance(IAttackable a, IAttackable b) {
            return Vector2.Distance(a.Position, b.Position) - a.Radius - b.Radius;
        }

    }
}
