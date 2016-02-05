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
            Move,
            AttackMove,
            Attack,
            Chase
        }

        UnitStates currentState = UnitStates.Idle;
        Unit controlledUnit;
        GameplayManager gm;

        public Path pathToFollow;
        float previousPathParam = 0;
        float lookAheadAmount = 20.0f;
        IAttackable attackTarget = null;

        public UnitController(Unit u, GameplayManager gm) {
            controlledUnit = u;
            this.gm = gm;
            u.Controller = this;
        }

        public void Detach() {
            controlledUnit = null;
        }
        

        public void Update(GameTime gameTime) {
            if (controlledUnit == null)
                return;
            float distance;
            switch (currentState) { 
                case UnitStates.Attack: //Attack a specific target (or chase if necessary)
                    controlledUnit.SetVelocity(Vector2.Zero);
                    if (attackTarget.IsDead()) {
                        currentState = UnitStates.Idle;
                        attackTarget = null;
                        break;
                    }
                    distance =  AttackableHelper.Distance(controlledUnit,attackTarget);
                    if (distance > controlledUnit.VisionRange) {
                        currentState = UnitStates.Idle;
                    }
                    else if (distance <= controlledUnit.AttackRange)
                    {
                        controlledUnit.Attack(attackTarget);
                    }
                    else {
                        currentState = UnitStates.Chase;
                    }
                    break;
                case UnitStates.Chase:
                    controlledUnit.SetVelocity(Vector2.Zero);
                    if (attackTarget.IsDead())
                    {
                        currentState = UnitStates.Idle;
                        attackTarget = null;
                        break;
                    }
                    distance = AttackableHelper.Distance(controlledUnit, attackTarget);
                    if (distance > controlledUnit.VisionRange) {
                        currentState = UnitStates.Idle;
                    }
                    else if (distance <= controlledUnit.AttackRange)
                    {
                        controlledUnit.Attack(attackTarget);
                    }
                    else {
                        currentState = UnitStates.Chase;
                    }
                    break;
                case UnitStates.HoldPosition: //Don't attack or move until ordered otherwise!
                    controlledUnit.SetVelocity(Vector2.Zero);
                    break;
                case UnitStates.Move: //Move to a location along a given path. Do not stop to attack/chase anything.
                    {
                        float newParam = pathToFollow.GetParam(controlledUnit.Position, previousPathParam);
                        Vector2 targetPosition = pathToFollow.GetPosition(newParam + lookAheadAmount);
                        if (Vector2.Distance(targetPosition,controlledUnit.Position)>0.001f)
                            controlledUnit.SetVelocity(Vector2.Normalize(targetPosition - controlledUnit.Position) * controlledUnit.MovementSpeed);
                        previousPathParam = newParam;
                        if (pathToFollow.GetLength() - newParam < 0.001f)
                            currentState = UnitStates.Idle;
                    }
                    break;
                case UnitStates.Idle: //Stand still, but attack/chase enemy units entering sight range.
                    {
                        controlledUnit.SetVelocity(Vector2.Zero);
                        IAttackable nearestTarget = gm.GetNearestTarget(controlledUnit, controlledUnit.AggroRange);
                        if (nearestTarget != null)
                        {
                            attackTarget = nearestTarget;
                            currentState = UnitStates.Attack;
                        }
                    }
                    break;
                case UnitStates.AttackMove: //Move to a location along a given path. Attack/chase any enemy unit entering sight range on the way.
                    break;
            }
        }

        public void AttackMove() { 
            
        }

        public void FollowPath(Path path) {
            if (path == null)
                return;
            pathToFollow = path;
            currentState = UnitStates.Move;
            previousPathParam = 0;
        }


    }
}
