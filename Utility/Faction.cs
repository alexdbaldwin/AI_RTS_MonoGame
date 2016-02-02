using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Factions
    {
        public static Color GetFactionColor(int faction) {
            switch (faction) { 
                case 0:
                    return Color.Blue;
                case 1:
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        public static Color GetFactionSelectionColor(int faction)
        {
            switch (faction)
            {
                case 0:
                    return Color.LightBlue;
                case 1:
                    return Color.Pink;
                default:
                    return Color.Gray;
            }
        } 

    }
}
