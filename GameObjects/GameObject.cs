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

        protected Vector2 position;
        protected Rectangle bounds;

        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Bounds {
            get { return bounds; }
        }

        public GameObject(Vector2 position) {
            this.position = position;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public bool Collides(GameObject other) {
            return bounds.Intersects(other.bounds);
        }

        public abstract void RecalculateBounds();

    }
}
