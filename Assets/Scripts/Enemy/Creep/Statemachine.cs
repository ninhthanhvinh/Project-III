using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Enemy
{
    public class StateMachine
    {
        public IState[] states;
        public Enemy enemy;
        public StateID currentState;

        public StateMachine(Enemy enemy)
        {
            this.enemy = enemy;
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
            GetState(currentState)?.Update(enemy);
        }

        public void ChangeState(StateID newState)
        {
            GetState(currentState)?.Exit(enemy);
            currentState = newState;
            GetState(currentState)?.Enter(enemy);
        }
    }


    public enum StateID
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public interface IState
    {
        StateID GetId();
        void Enter(Enemy enemy);
        void Update(Enemy enemy);
        void Exit(Enemy enemy);

    }
}
