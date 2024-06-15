using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class GoToTarget : ActionNode
{
    [SerializeField] private float targetRadius;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.agent.SetDestination(context.target.position);
        context.transform.LookAt(context.target);
        if (Vector3.Distance(context.transform.position, context.target.position) < targetRadius)
        {
            context.agent.isStopped = true;
            return State.Success;
        }

        return State.Running;
    }
}
