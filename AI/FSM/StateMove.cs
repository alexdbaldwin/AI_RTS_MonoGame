using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame.AI.FSM
{
    class StateMove : UnitFSMState
    {
        float previousPathParam = 0;
        float lookAheadAmount = 20.0f;

        public StateMove(UnitController controller) : base(FSMStates.Move, controller) { }

        public override void Enter()
        {
            previousPathParam = 0;
        }
        public override void Exit()
        {

        }
        public override void Update(GameTime gameTime)
        {
            float newParam = controller.PathToFollow.GetParam(controller.ControlledUnit.Position, previousPathParam);
            Vector2 targetPosition = controller.PathToFollow.GetPosition(newParam + lookAheadAmount);
            if (Vector2.Distance(targetPosition, controller.ControlledUnit.Position) > 0.001f)
                controller.ControlledUnit.SetVelocity(Vector2.Normalize(targetPosition - controller.ControlledUnit.Position) * controller.ControlledUnit.MovementSpeed);
            previousPathParam = newParam;
            if (controller.PathToFollow.GetLength() - newParam < 0.001f)
            {
                controller.PathToFollow = null;
            }
        }
        public override void Init()
        {
            previousPathParam = 0;
            lookAheadAmount = 20.0f;
        }
        public override FSMStates CheckTransitions()
        {
            if (controller.PathToFollow == null) {
                return FSMStates.Idle;
            }

            return FSMStates.Move;
        }
    }
}
