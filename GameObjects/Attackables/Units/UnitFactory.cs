using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    static class UnitFactory
    {

        public static Unit SpawnUnit(GameplayManager gm, UnitTypes type, Vector2 position, int faction, World world) {
            switch (type)
            {
                case UnitTypes.Ranged:
                    return CreateRangedUnit(gm, position, faction, world);
                case UnitTypes.Melee:
                    return CreateMeleeUnit(gm, position, faction, world);
                default:
                    return CreateRangedUnit(gm, position, faction, world);
            }
        }

        public static Unit CreateRangedUnit(GameplayManager gm, Vector2 position,int faction, World world){
            Unit u = new Unit(gm, position, faction, world, 60.0f,80.0f,100.0f,6,0.5f,5.0f,0.3f,100,5.0f);
            return u;
        }

        public static Unit CreateMeleeUnit(GameplayManager gm, Vector2 position, int faction, World world)
        {
            Unit u = new Unit(gm, position, faction, world, 2.0f, 80.0f, 100.0f, 12, 0.8f, 6.0f, 0.2f, 50, 8.0f);
            return u;
        }

    }
}
