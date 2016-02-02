using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Unit : GameObject, IAttackable, ISelectable
    {

        public BoundingCircle boundingCircle;
        protected int faction;
        protected int HP = 100;
        protected float range = 60.0f;
        protected float attackDelay = 0.5f;
        protected int damage = 6;

        protected bool selected = false;

        public int Faction {
            get {
                return faction;
            }
        }

        public UnitController Controller
        {
            get;
            set;
        }

        public Unit(Vector2 position, int faction) : base(position) {
            this.faction =  faction;

            bounds = new Rectangle((int)position.X - 5, (int)position.Y - 5, 10, 10);
            boundingCircle = new BoundingCircle(position, 5);
            
        }

        public override void RecalculateBounds() {
            bounds = new Rectangle((int)position.X - 5, (int)position.Y - 5, 10, 10);
            boundingCircle.center = position;
        }

        public void AttackMove() { 
            
        }

        public void Move() { 
        
        }

        public void Attack(GameObject gameObject) { 
        
        }

        public void HoldPosition() { 
            
        }

        public override void Update(GameTime gameTime)
        {
            bounds = new Rectangle((int)position.X - 5, (int)position.Y - 5, 10, 10);
            boundingCircle.center = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(selected)
                spriteBatch.Draw(AssetManager.GetTexture("circle_100x100"), position, null, Factions.GetFactionSelectionColor(faction), 0, new Vector2(50, 50), (bounds.Width*2.0f) / 100.0f, SpriteEffects.None, 0);
        
            spriteBatch.Draw(AssetManager.GetTexture("circle_100x100"), position, null, Factions.GetFactionColor(faction), 0, new Vector2(50, 50), bounds.Width / 100.0f, SpriteEffects.None, 0);
        }



        public void DealDamage(int dmg)
        {
            throw new NotImplementedException();
        }

        public bool IsDead()
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            selected = true;
        }

        public void Deselect()
        {
            selected = false;
        }

        public Point GetSelectionPoint() {
            return new Point((int)Position.X, (int)Position.Y);
        }

        public Rectangle GetBounds() {
            return bounds;
        }
    }
}
