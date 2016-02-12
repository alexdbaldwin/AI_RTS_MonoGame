using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame.AI.FSM
{
    abstract class UnitFSMState : FSMState
    {
        protected UnitController controller;

        public UnitFSMState(FSMState.FSMStates type, UnitController controller) :base (type){
            this.controller = controller;
        }

    }
}
