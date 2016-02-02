using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class UnitController
    {
        public enum UnitStates { 
            Idle,
            HoldPosition,
            FollowPath,
            AttackMove,
            Attack
        }

        UnitStates currentState = UnitStates.Idle;
        Unit controlledUnit;

        public Path pathToFollow;
        float previousPathParam = 0;
        float lookAheadAmount = 20.0f;
        float unitSpeed = 100.0f;

        public UnitController(Unit u) {
            controlledUnit = u;
            u.Controller = this;

        }
        

        public void Update(GameTime gameTime) {
            switch (currentState) { 
                case UnitStates.Attack:
                    break;
                case UnitStates.HoldPosition:
                    break;
                case UnitStates.FollowPath:
                    float newParam = pathToFollow.GetParam(controlledUnit.Position, previousPathParam);
                    Vector2 targetPosition = pathToFollow.GetPosition(newParam + lookAheadAmount);
                    controlledUnit.Position += Vector2.Normalize(targetPosition - controlledUnit.Position) * (float)gameTime.ElapsedGameTime.TotalSeconds * unitSpeed;
                    previousPathParam = newParam;
                    break;
                case UnitStates.Idle:
                    break;
                case UnitStates.AttackMove:
                    break;
            }
        }

        public void FollowPath(Path path) {
            if (path == null)
                return;
            pathToFollow = path;
            currentState = UnitStates.FollowPath;
            previousPathParam = 0;
        }


    }
}
