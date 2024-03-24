using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using RPG.Statemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IAgent
{
    public NavMeshAgent GetAgent();
    public void ChangeState(StateID newState);
}
