using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    

    class Unit : Attackable
    {
        protected float attackRange = 60.0f;
        protected float aggroRange = 80.0f;
        
        protected float attackDelay = 0.5f;
        protected float attackTimer = 0.0f;
        protected float movementSpeed = 5.0f;
        protected float attackSpeed = 0.1f; //The amount of time between the attack being initiated and damage being applied
        protected int attackDamage = 6;
        protected float productionTime = 10.0f;
        protected float cost = 100.0f;


        public float AttackRange { get { return attackRange; } }
        public float AggroRange { get { return aggroRange; } }
        public int AttackDamage { get { return attackDamage; } }
        public float AttackDelay { get { return attackDelay; } }
        public float MovementSpeed { get { return movementSpeed; } }
        public float ProductionTime { get { return productionTime; } set { productionTime = value; } }
        public float Cost { get { return cost; } }

       

        public UnitController Controller
        {
            get;
            set;
        }

        public Unit(GameplayManager gm, World world, Vector2 position, int faction, int HP, float radius, float attackRange, float aggroRange, float visionRange, int attackDamage, float attackDelay, float movementSpeed, float attackSpeed, float productionTime, float cost)
            : base(gm, position, faction, HP, visionRange, world)
        {
            this.attackRange = attackRange;
            this.aggroRange = aggroRange;
            this.attackDamage = attackDamage;
            this.attackDelay = attackDelay;
            this.movementSpeed = movementSpeed;
            this.attackSpeed = attackSpeed;
            this.radius = radius;
            this.productionTime = productionTime;
            this.cost = cost;

            body = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(radius), 10/*????*/, ConvertUnits.ToSimUnits(position));
            body.BodyType = BodyType.Dynamic;
            body.CollisionCategories = Category.All;
            body.CollidesWith = Category.All;


            //A unit must be manually enabled when spawned
            Disable();

            
        }

        public void Enable() {
            body.Enabled = true;
        }

        public void Disable() {
            body.Enabled = false;
        }

        public void SetVelocity(Vector2 v) {
            body.LinearVelocity = v;
        }


        public void AttackMove() { 
            
        }

        public void Move() { 
        
        }

        public virtual void Attack(IAttackable target) {
            if (attackTimer <= 0) {
                target.DealDamage(attackDamage, attackSpeed);
                attackTimer = attackDelay;
                gm.VFX.SpawnProjectile(Position, attackSpeed, target);
            }
        }

        public void HoldPosition() { 
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!body.Enabled)
                return;
            //Decrease attack cooldown
            attackTimer = MathHelper.Clamp(attackTimer - (float)gameTime.ElapsedGameTime.TotalSeconds, 0, attackDelay);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!body.Enabled)
                return;
            if(selected)
                spriteBatch.Draw(AssetManager.GetTexture("circle_100x100"), ConvertUnits.ToDisplayUnits(body.Position), null, Factions.GetFactionSelectionColor(faction), 0, new Vector2(50, 50), 4*radius / 100.0f, SpriteEffects.None, 0);
            Vector2 v = ConvertUnits.ToDisplayUnits(body.Position);
            spriteBatch.Draw(AssetManager.GetTexture("circle_100x100"), ConvertUnits.ToDisplayUnits(body.Position), null, Factions.GetFactionColor(faction), 0, new Vector2(50, 50), 2*radius / 100.0f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }
        


        public override bool Contains(Vector2 point) {
            return Vector2.Distance(point, ConvertUnits.ToDisplayUnits(body.Position)) < radius;
        }

        public Attackable GetNearestTargetInVisionRange()
        {
            return gm.GetNearestTarget(this, visionRange);
        }

        public Attackable GetNearestTargetInAggroRange()
        {
            return gm.GetNearestTarget(this, aggroRange);
        }

        public Attackable GetNearestTargetInAttackRange()
        {
            return gm.GetNearestTarget(this, attackRange);
        }

    }
}
