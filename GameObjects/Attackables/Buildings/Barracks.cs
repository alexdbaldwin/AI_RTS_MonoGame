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
        //To implement;
        Vector2 rallyPoint;
        bool rallyPointSet = false;

        Queue<Unit> productionQueue = new Queue<Unit>();

        public Barracks(GameplayManager gm, int gridX, int gridY, int faction, World world, Grid grid)
            : base(gm, gridX, gridY, faction, world, 2, 2, 500, 150.0f, grid)
        { 
            
        }

        public override void Update(GameTime gameTime)
        {
            if (productionQueue.Count > 0) {
                productionQueue.First().ProductionTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                float timeLeft = productionQueue.First().ProductionTime;
                if (timeLeft <= 0) {
                    SpawnUnit(productionQueue.Dequeue());
                    if (productionQueue.Count > 0)
                    {
                        //Don't cheat the next unit out of any time
                        productionQueue.First().ProductionTime += timeLeft;
                    }
                }
            }

            base.Update(gameTime);
        }

        public void QueueRangedUnit() {
            QueueUnit(UnitFactory.CreateRangedUnit(gm, Vector2.Zero, faction, world));
        }

        public void QueueMeleeUnit()
        {
            QueueUnit(UnitFactory.CreateMeleeUnit(gm, Vector2.Zero, faction, world));
        }

        private void QueueUnit(Unit u) {
            productionQueue.Enqueue(u);
        }

        /// <summary>
        /// Move unit to a suitable location and then enable it
        /// </summary>
        /// <param name="u"></param>
        private void SpawnUnit(Unit u) {
            for (int i = gridX - 1; i < gridX + tileWidth + 1; i++) {
                for (int j = gridY - 1; j < gridY + tileHeight + 1; i++) {
                    if (grid.IsPassable(i, j)) {
                        gm.SpawnUnit(u, new Vector2(Grid.TileSize * (i + 0.5f), Grid.TileSize * (j + 0.5f)) + Vector2.Normalize(new Vector2((float)Game1.rand.NextDouble() * 2 - 1, (float)Game1.rand.NextDouble() * 2 - 1))*0.1f);
                        return;
                    }
                }
            }
            
        }

    }
}
