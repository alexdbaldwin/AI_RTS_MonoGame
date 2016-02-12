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
        protected List<IAttackable> selection = new List<IAttackable>();
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

        public virtual void Deselect(IAttackable s) {
            selection.Remove(s);
        }


    }
}
