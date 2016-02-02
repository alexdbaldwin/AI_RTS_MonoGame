using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    interface ISelectable
    {
        void Select();
        void Deselect();
        Point GetSelectionPoint();
        Rectangle GetBounds();
    }
}
