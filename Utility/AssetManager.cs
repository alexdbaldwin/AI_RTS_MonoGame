using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class AssetManager
    {

        static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void AddTexture(string key, Texture2D value)
        {
            textures.Add(key, value);
        }

        public static Texture2D GetTexture(string key)
        {
            return textures[key];
        }

        public static void AddFont(string key, SpriteFont value)
        {
            fonts.Add(key, value);
        }

        public static SpriteFont GetFont(string key)
        {
            return fonts[key];
        }

    }
}
