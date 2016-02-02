using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{

    abstract class ArmyController
    {
        protected GameplayManager gm;
        protected List<ISelectable> selection = new List<ISelectable>();
        protected int faction;

        public int Faction {
            get {
                return faction;
            }
        }

        public ArmyController(GameplayManager gm, int faction) {
            this.gm = gm;
            this.faction = faction;
        }

        public abstract void Update(GameTime gameTime);

    }
}
