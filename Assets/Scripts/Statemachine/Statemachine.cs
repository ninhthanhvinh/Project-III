using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Statemachine
{
    public class StateMachine
    {
        public IState[] states;
        public NonPlayableCharacter agent;
        public StateID currentState;

        public StateMachine(NonPlayableCharacter agent)
        {
            this.agent = agent;
            int numStates = System.Enum.GetNames(typeof(StateID)).Length;
            states = new IState[numStates];
        }

        public void RegisterState(IState state)
        {
            int index = (int)state.GetId();
            states[index] = state;
        }

        public IState GetState(StateID stateID)
        {
            int index = (int)stateID;
            return states[index];
        }
        public void Update()
        {
            GetState(currentState)?.Update(agent);
            Debug.Log(currentState);
        }

        public void ChangeState(StateID newState)
        {
            GetState(currentState)?.Exit(agent);
            currentState = newState;
            GetState(currentState)?.Enter(agent);
        }
    }


    public enum StateID
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public interface IState
    {
        StateID GetId();
        void Enter(NonPlayableCharacter agent);
        void Update(NonPlayableCharacter agent);
        void Exit(NonPlayableCharacter agent);

    }
}
