using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Statemachine;

public class NonPlayableCharacter : MonoBehaviour
{
    protected StateMachine stateMachine;
    public StateMachine StateMachine => stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new PatrolState());
        //stateMachine.RegisterState(new ChaseState());
        //stateMachine.RegisterState(new AttackState());
        //stateMachine.RegisterState(new DeadState());
        stateMachine.ChangeState(StateID.Idle);
    }

    public void Update()
    {
        StateMachine.Update();
    }
}
