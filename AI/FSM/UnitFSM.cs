using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame.AI.FSM
{
    class UnitFSM : FSM
    {
        public UnitFSM(UnitController controller) {
            StateIdle idleState = new StateIdle(controller);
            AddState(idleState);
            AddState(new StateMove(controller));
            AddState(new StateAttack(controller));
            AddState(new StateAttackMove(controller));
            AddState(new StateChase(controller));
            AddState(new StateHoldPosition(controller));
            defaultState = idleState;
            Reset();
        }
    }
}
