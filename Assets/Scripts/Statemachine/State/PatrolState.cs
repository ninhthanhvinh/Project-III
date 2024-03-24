using RPG.Enemy;
using RPG.Statemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    const int PATROL_DISTANCE = 10;
    private NavMeshAgent navAgent;
    public void Enter(NonPlayableCharacter agent)
    {
        navAgent = agent.GetComponent<NavMeshAgent>();
        int patrolX = Random.Range(-PATROL_DISTANCE, PATROL_DISTANCE);
        int patrolZ = Random.Range(-PATROL_DISTANCE, PATROL_DISTANCE);
        Vector3 patrolPoint = new Vector3(agent.transform.position.x + patrolX, agent.transform.position.y, agent.transform.position.z + patrolZ);
        navAgent.SetDestination(patrolPoint);
    }

    public void Exit(NonPlayableCharacter agent)
    {
        
    }

    public StateID GetId()
    {
        return StateID.Patrol;
    }

    public void Update(NonPlayableCharacter agent)
    {
        if (navAgent.remainingDistance < 1)
        {
            agent.StateMachine.ChangeState(StateID.Idle);
        }
    }
}
