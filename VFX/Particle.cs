using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Particle
    {
        Texture2D tex;
        Vector2 position;
        Vector2 velocity;
        Vector2 acceleration;
        float lifetime;


        float lifeMax;
        Color startColor;
        Color endColor;
        float startScale;
        float endScale;

        public bool Alive {
            get { return lifetime > 0; }
        }

        public Particle(Texture2D tex, Vector2 position, Vector2 velocity, float lifetime, Color startColor, Color endColor, float startScale, float endScale, Vector2 acceleration)
        {
            this.tex = tex;
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.lifetime = lifetime;
            lifeMax = lifetime;
            this.startColor = startColor;
            this.endColor = endColor;
            this.startScale = startScale;
            this.endScale = endScale;
        }

        public void Update(float dt) {
            lifetime -= dt;

            //Move particle
            position += velocity * dt;
            velocity += acceleration * dt;
            
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(tex, position, null, Color.Lerp(startColor,endColor, 1 - lifetime/lifeMax), 0, new Vector2(tex.Width/2,tex.Height/2),MathHelper.Lerp(startScale,endScale, 1- lifetime/lifeMax),SpriteEffects.None,0);
        }
    }
}
