using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame.GameObjects.Attackables.Buildings
{
    class PowerPlant : Building
    {

        public PowerPlant(GameplayManager gm, int gridX, int gridY, int faction, World world, Grid grid)
            : base(gm, gridX, gridY, faction, world, 2, 2, 300,100.0f, grid)
        { 
            
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

    }
}
