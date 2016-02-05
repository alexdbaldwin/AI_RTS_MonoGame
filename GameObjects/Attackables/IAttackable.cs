using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    interface IAttackable
    {
        Vector2 Position { get; }
        float Radius { get; }
        int Faction { get; }
        void DealDamage(int dmg, float delay);
        bool IsDead();
        void Select();
        void Deselect();
        Vector2 GetSelectionPoint();
        bool Contains(Vector2 point);
    }
}
