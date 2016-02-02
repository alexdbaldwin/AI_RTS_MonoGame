using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    static class DebugDraw
    {

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 v1, Vector2 v2) {
            Vector2 v = v2 - v1;
            spriteBatch.Draw(AssetManager.GetTexture("pixel"), v1, null, Color.Red, (float)Math.Atan2(v.Y,v.X), Vector2.Zero, new Vector2(Vector2.Distance(v1, v2), 1), SpriteEffects.None, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle r) {
            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(r.Left, r.Top));
            points.Add(new Vector2(r.Right, r.Top));
            points.Add(new Vector2(r.Right, r.Bottom));
            points.Add(new Vector2(r.Left, r.Bottom));
            for (int i = 0; i < 4; i++) {
                DrawLine(spriteBatch, points[i], points[(i + 1) % 4]);
            }
        }

    }
}
