using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class ProjectileEffect : VisualEffect
    {
        ParticleEmitter emitter;
        IAttackable target;
        float timeLeft;

        public ProjectileEffect(Vector2 position, float time, IAttackable target) {
            timeLeft = time;
            this.target = target;
            emitter = ParticleEmitter.SpawnGenericProjectileEmitter(position);
        }

        public override bool IsExpired()
        {
            return timeLeft <= 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (timeLeft <= 0.0f)
                return;
            if (target.IsDead())
            {
                timeLeft = 0.0f;
                return;
            }
            emitter.Position = Vector2.Lerp(emitter.Position, target.Position, (float)gameTime.ElapsedGameTime.TotalSeconds / timeLeft);
            timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            emitter.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            emitter.Draw(spriteBatch);
        }

        
    }
}
