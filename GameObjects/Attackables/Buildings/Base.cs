using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Base : Building
    {
        public Base(GameplayManager gm, int gridX, int gridY, int faction, World world, Grid grid) : base(gm,gridX,gridY,faction,world,3,3,1000, grid) { 
        
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
