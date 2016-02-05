using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    abstract class Attackable : GameObject, IAttackable
    {
        protected GameplayManager gm;
        protected int faction;
        protected int HP = 100;
        protected int maxHP = 100;
        protected List<DelayedDamage> delayedDamage = new List<DelayedDamage>();
        protected bool selected = false;
        protected float radius;

        public float Radius
        {
            get { return radius; }
        }

        public int Faction
        {
            get { return faction; }
        }

        public Attackable(GameplayManager gm, Vector2 position, int faction, int HP, World world) : base(world) {
            this.gm = gm;
            this.faction = faction;
            this.maxHP = HP;
            this.HP = HP;
        }

        public virtual void DealDamage(int dmg, float delay = 0.0f)
        {
            if (delay == 0.0f)
                HP -= dmg;
            else
            {
                delayedDamage.Add(new DelayedDamage(dmg, delay));
                delayedDamage.Sort();
            }
        }

        public bool IsDead()
        {
            return HP <= 0;
        }

        public void Select()
        {
            selected = true;
        }

        public void Deselect()
        {
            selected = false;
        }

        public Vector2 GetSelectionPoint()
        {
            return Position;
        }

        public abstract bool Contains(Vector2 point);

        public override void Update(GameTime gameTime)
        {
            //Apply delayed damage
            for (int i = 0; i < delayedDamage.Count; ++i)
            {
                if (delayedDamage[i].DecreaseTime((float)gameTime.ElapsedGameTime.TotalSeconds))
                {
                    HP -= delayedDamage[i].Dmg;
                    delayedDamage.RemoveAt(i--);
                }
            }
        }

    }
}
