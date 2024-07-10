using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPosition : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 0.1f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    protected override void OnStart() {
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.moveToPosition.position;
        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        context.agent.destination = blackboard.moveToPosition.position;

        if (context.agent.pathPending) {
            if (context.animator != null)
                context.animator.SetFloat("speed", 1f);
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance) {
            if (context.animator != null)
                context.animator.SetFloat("speed", 0f);
            return State.Success;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
            if (context.animator != null)
                context.animator.SetFloat("speed", 0f);
            return State.Failure;
        }

        return State.Running;
    }
}
