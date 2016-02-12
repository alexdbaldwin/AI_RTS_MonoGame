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
        bool aPressed = false;

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
                if (aPressed) {
                    //A-move selected units
                    foreach (IAttackable s in selection)
                    {
                        if (s is Unit)
                            (s as Unit).Controller.AttackMove(KeyMouseReader.mouseState.Position.ToVector2());
                    }
                }
                else
                {
                    selection.Clear();
                    IAttackable s = gm.ClickSelect(KeyMouseReader.mouseState.Position.ToVector2(), faction);
                    if (s != null)
                        selection.Add(s);
                }
                selectionBox = new Rectangle();
            }
            else if (KeyMouseReader.LeftButtonReleased()){
                selection = gm.BoxSelect(selectionBox, faction);
                selectionBox = new Rectangle();
            }

            if (KeyMouseReader.LeftButtonReleased())
                aPressed = false;

            if (KeyMouseReader.KeyPressed(Keys.S)) { //Stop
                foreach (IAttackable s in selection)
                {
                    if (s is Unit)
                        (s as Unit).Controller.Stop();
                }
            }
            if (KeyMouseReader.KeyPressed(Keys.A)) { //Attack
                aPressed = true;
            }
            if (KeyMouseReader.KeyPressed(Keys.H)) { //Hold position
                foreach (IAttackable s in selection)
                {
                    if (s is Unit)
                        (s as Unit).Controller.HoldPosition();
                }
            }


            //UGLY!!!
            if (KeyMouseReader.RightClick()) {
                Attackable a = gm.GetAttackableAtLocation(KeyMouseReader.mouseState.Position.ToVector2(),faction);
                if (a != null)
                {
                    foreach (IAttackable s in selection)
                    {
                        if (s is Unit)
                            (s as Unit).Controller.Attack(a);
                    }
                }
                else {
                    foreach (IAttackable s in selection)
                    {
                        if (s is Unit)
                            (s as Unit).Controller.FollowPath(gm.GetPath((s as Unit).Position, KeyMouseReader.mouseState.Position.ToVector2()));
                    }
                }

                
            }
        }

    }
}
