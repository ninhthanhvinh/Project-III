using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class RandomPosition : ActionNode
{
    public Vector2 min = Vector2.one * -10;
    public Vector2 max = Vector2.one * 10;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float x = Random.Range(min.x, max.x);
        float z = Random.Range(min.y, max.y);
        
        blackboard.moveToPosition.position = new Vector3(x, blackboard.moveToPosition.position.y, z);
        return State.Success;
    }
}
