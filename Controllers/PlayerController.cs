using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class PlayerController : ArmyController
    {

        Rectangle selectionBox = new Rectangle();

        public Rectangle SelectionBox {
            get {
                return selectionBox;
            }
        }

        public PlayerController(GameplayManager gm, int faction) : base(gm, faction){ 
            
        }

        public override void Update(GameTime gameTime) {


            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed) {
                selectionBox = new Rectangle(
                    Math.Min(KeyMouseReader.mouseState.X, KeyMouseReader.leftMouseDownPosition.X),
                    Math.Min(KeyMouseReader.mouseState.Y, KeyMouseReader.leftMouseDownPosition.Y),
                    Math.Abs(KeyMouseReader.mouseState.X - KeyMouseReader.leftMouseDownPosition.X),
                    Math.Abs(KeyMouseReader.mouseState.Y - KeyMouseReader.leftMouseDownPosition.Y)); 
            }
            
            if (KeyMouseReader.LeftClickInPlace())
            {
                selection.Clear();
                ISelectable s = gm.ClickSelect(KeyMouseReader.mouseState.Position.ToVector2());
                if(s != null)
                    selection.Add(s);
                selectionBox = new Rectangle();
            }
            else if (KeyMouseReader.LeftButtonReleased()){
                selection = gm.BoxSelect(selectionBox);
                selectionBox = new Rectangle();
            }


            //UGLY!!!
            if (KeyMouseReader.RightClick()) {
                foreach (ISelectable s in selection)
                {
                    if(s is Unit)
                        (s as Unit).Controller.FollowPath(gm.GetPath((s as Unit).Position, KeyMouseReader.mouseState.Position.ToVector2()));
                }
            }
        }

    }
}
