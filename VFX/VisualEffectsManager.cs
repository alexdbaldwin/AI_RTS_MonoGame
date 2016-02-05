using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class VisualEffectsManager
    {
        List<VisualEffect> vfx = new List<VisualEffect>();

        public VisualEffectsManager() { 
            
        }

        public void Update(GameTime gameTime) {
            for (int i = 0; i < vfx.Count; ++i) {
                vfx[i].Update(gameTime);
                if (vfx[i].IsExpired())
                    vfx.RemoveAt(i--);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (VisualEffect v in vfx)
                v.Draw(spriteBatch);
        }

        public void SpawnProjectile(Vector2 position, float time, IAttackable target) {
            vfx.Add(new ProjectileEffect(position, time, target));
        }

    }
}
