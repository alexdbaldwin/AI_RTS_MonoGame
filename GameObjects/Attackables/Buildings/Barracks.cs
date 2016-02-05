using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Barracks : Building
    {

        public Barracks(GameplayManager gm, int gridX, int gridY, int faction, World world, Grid grid)
            : base(gm, gridX, gridY, faction, world, 2, 2, 500, grid)
        { 
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
