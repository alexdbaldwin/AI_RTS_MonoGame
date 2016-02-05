using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    abstract class GameObject
    {

        protected Body body;
        protected World world;

        public Vector2 Position {
            get { return ConvertUnits.ToDisplayUnits(body.Position); }
        }


        public GameObject(World world) {
            this.world = world;
        }

        public void DestroyBody() {
            world.RemoveBody(body);
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public static float Distance(GameObject a, GameObject b) {
            return (Vector2.Distance(ConvertUnits.ToDisplayUnits(a.body.Position), ConvertUnits.ToDisplayUnits(b.body.Position)));
        }

    }
}
