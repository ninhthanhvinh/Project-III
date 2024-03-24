using RPG.Statemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Statemachine
{
    public class IdleState : IState
    {
        private float idleTime = 1f;
        private float timer = 0f;
        public void Enter(NonPlayableCharacter agent)
        {
            timer = idleTime;
        }

        public void Exit(NonPlayableCharacter agent)
        {
            
        }

        public StateID GetId()
        {
            return StateID.Idle;
        }

        public void Update(NonPlayableCharacter agent)
        {
            if (timer <= 0)
            {
                agent.StateMachine.ChangeState(StateID.Patrol);
            }
            timer -= Time.deltaTime;
        }
    }
}