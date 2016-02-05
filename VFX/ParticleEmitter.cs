using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class ParticleEmitter
    {
        List<Particle> particles = new List<Particle>();
        Texture2D particleTexture;
        Vector2 position;
        float spawnDelay;
        float spawnTimer;
        float angleMax;
        float angleMin;
        float speedMin;
        float speedMax;
        float lifetimeMin;
        float lifetimeMax;
        Vector2 acceleration;
        Color startColor;
        Color endColor;
        float startScale;
        float endScale;

        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        Random rand = new Random();

        public ParticleEmitter(Texture2D particleTexture, Vector2 position) {
            this.particleTexture = particleTexture;
            this.position = position;
            spawnDelay = 0.001f;
            spawnTimer = spawnDelay;
            angleMax = 120;
            angleMin = 60;
            speedMin = 90;
            speedMax = 150;
            lifetimeMin = 0.6f;
            lifetimeMax = 1.2f;
            startColor = Color.White;
            endColor = Color.Blue;
            endColor.A = 0;
            startScale = 0.3f;
            endScale = 2.0f;
            acceleration = Vector2.Zero;
        }

        public void Update(GameTime gameTime) {
            //Spawn new particles
            spawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (spawnTimer <= 0) {
                spawnTimer += spawnDelay;
                SpawnParticle();
            }

            //Update and remove expired particles
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (!particles[i].Alive)
                {
                    particles.RemoveAt(i--);
                }
            }
        }

        public void Clear() {
            particles.Clear();
        }

        private void SpawnParticle() {
            Vector2 velocity;
            float angle = (float)rand.NextDouble() * (angleMax - angleMin) + angleMin;
            velocity.X = (float)Math.Cos(MathHelper.ToRadians(angle));
            velocity.Y = -(float)Math.Sin(MathHelper.ToRadians(angle));
            velocity.Normalize();
            float speed = (float)rand.NextDouble() * (speedMax - speedMin) + speedMin;
            velocity *= speed;
            //Vector2 acceleration = Vector2.Zero;
            float lifetime = (float)rand.NextDouble() * (lifetimeMax - lifetimeMin) + lifetimeMin;
            particles.Add(new Particle(particleTexture, position, velocity, lifetime,startColor,endColor,startScale,endScale,acceleration));
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < particles.Count; i++) {
                particles[i].Draw(spriteBatch);
            }
        }


        //Factory methods for making a few different emitter types
        public static ParticleEmitter SpawnSmokeEmitter(Texture2D tex, Vector2 position) {
            ParticleEmitter pe = new ParticleEmitter(tex, position);
            pe.spawnDelay = 0.001f;
            pe.spawnTimer = pe.spawnDelay;
            pe.angleMax = 120;
            pe.angleMin = 60;
            pe.speedMin = 90;
            pe.speedMax = 150;
            pe.lifetimeMin = 0.6f;
            pe.lifetimeMax = 1.2f;
            pe.startColor = Color.White;
            pe.endColor = Color.Black;
            //pe.endColor.A = 120;
            pe.startScale = 0.6f;
            pe.endScale = 2.0f;
            pe.acceleration = new Vector2(0, 600);
            return pe;
        }

        public static ParticleEmitter SpawnGenericProjectileEmitter(Vector2 position) {
            ParticleEmitter pe = new ParticleEmitter(AssetManager.GetTexture("softparticle"), position);
            pe.spawnDelay = 0.0002f;
            pe.spawnTimer = pe.spawnDelay;
            pe.angleMin = 0;
            pe.angleMax = 360;
            pe.speedMin = 40;
            pe.speedMax = 80;
            pe.lifetimeMin = 0.05f;
            pe.lifetimeMax = 0.1f;
            pe.startColor = Color.Orange;
            pe.endColor = Color.Maroon;
            pe.endColor.A = 200;
            pe.startScale = 0.09f;
            pe.endScale = 0.03f;
            return pe;
        }

        public static ParticleEmitter SpawnFireEmitter(Texture2D tex, Vector2 position)
        {
            ParticleEmitter pe = new ParticleEmitter(tex, position);
            pe.spawnDelay = 0.002f;
            pe.spawnTimer = pe.spawnDelay;
            pe.angleMin = 70;
            pe.angleMax = 110;
            pe.speedMin = 150;
            pe.speedMax = 250;
            pe.lifetimeMin = 0.3f;
            pe.lifetimeMax = 0.9f;
            pe.startColor = Color.Yellow;
            pe.endColor = Color.Red;
            pe.endColor.A = 50;
            pe.startScale = 2.5f;
            pe.endScale = 0.3f;
            return pe;
        }

        public static ParticleEmitter SpawnExplosionEmitter(Texture2D tex, Vector2 position)
        {
            ParticleEmitter pe = new ParticleEmitter(tex, position);
            pe.spawnDelay = 0.001f;
            pe.spawnTimer = pe.spawnDelay;
            pe.angleMin = 0;
            pe.angleMax = 360;
            pe.speedMin = 300;
            pe.speedMax = 900;
            pe.lifetimeMin = 0.2f;
            pe.lifetimeMax = 0.5f;
            pe.startColor = Color.Yellow;
            pe.endColor = Color.Red;
            pe.endColor.A = 120;
            pe.startScale = 2.5f;
            pe.endScale = 0.3f;
            return pe;
        }

        public static ParticleEmitter SpawnShowerEmitter(Texture2D tex, Vector2 position)
        {
            ParticleEmitter pe = new ParticleEmitter(tex, position);
            pe.spawnDelay = 0.0005f;
            pe.spawnTimer = pe.spawnDelay;
            pe.angleMin = -135;
            pe.angleMax = -45;
            pe.speedMin = 500;
            pe.speedMax = 800;
            pe.lifetimeMin = 0.1f;
            pe.lifetimeMax = 0.3f;
            pe.startColor = Color.White;
            pe.endColor = Color.Blue;
            //pe.endColor.A = 120;
            pe.startScale  = 0.15f;
            pe.endScale = 0.08f;
            return pe;
        }

        public static ParticleEmitter SpawnToxicEmitter(Texture2D tex, Vector2 position)
        {
            ParticleEmitter pe = new ParticleEmitter(tex, position);
            pe.spawnDelay = 0.05f;
            pe.spawnTimer = pe.spawnDelay;
            pe.angleMin = 0;
            pe.angleMax = 360;
            pe.speedMin = 50;
            pe.speedMax = 100;
            pe.lifetimeMin = 3f;
            pe.lifetimeMax = 5f;
            pe.startColor = Color.LimeGreen;
            pe.endColor = Color.Teal;
            //pe.endColor.A = 120;
            pe.startScale = 3f;
            pe.endScale = 5f;
            return pe;
        }

    }
}
