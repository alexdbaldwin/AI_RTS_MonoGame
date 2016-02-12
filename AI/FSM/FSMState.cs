using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame.AI.FSM
{
    abstract class FSMState
    {
        public enum FSMStates {
            Attack,
            AttackMove,
            Move,
            HoldPosition,
            Chase,
            Idle
        }

        protected FSMStates type;

        public FSMStates Type {
            get { return type; }
        }

        public FSMState(FSMStates type) {
            this.type = type;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update(GameTime gameTime);
        public abstract void Init();
        public abstract FSMStates CheckTransitions();

    }
}
