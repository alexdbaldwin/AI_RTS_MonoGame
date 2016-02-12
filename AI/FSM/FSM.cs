using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame.AI.FSM
{
    class FSM
    {
        protected List<FSMState> states = new List<FSMState>();
        protected FSMState currentState;
        protected FSMState defaultState;

        public FSM() { }

        public void Update(GameTime gameTime) {
            if (currentState == null)
                currentState = defaultState;
            if (currentState == null)
                return;

            FSMState.FSMStates goalType = currentState.CheckTransitions();
            ChangeState(goalType);
            currentState.Update(gameTime);
        }

        public void AddState(FSMState state) {
            states.Add(state);
        }

        public void SetDefaultState(FSMState state) {
            defaultState = state;
        }

        private FSMState GetGoalState(FSMState.FSMStates goalState) {
            foreach (FSMState state in states) {
                if (state.Type == goalState)
                    return state;
            }
            return null;
        }

        public void Reset() {
            if (currentState != null)
                currentState.Exit();
            currentState = defaultState;
            foreach (FSMState state in states)
                state.Init();
            if (currentState != null)
                currentState.Enter();
        }

        public void ChangeState(AI_RTS_MonoGame.AI.FSM.FSMState.FSMStates state, bool reenter = false) {
            if (reenter || state != currentState.Type)
            {
                currentState.Exit();
                currentState = GetGoalState(state);
                currentState.Enter();
            }
        }

    }
}
