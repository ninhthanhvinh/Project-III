using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class FieldOfViewCheck : ActionNode
    {
        public float radius;
        public float angle;
        private Transform target;
        protected override void OnStart()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            Vector3 toTarget = target.position - context.transform.position;

            if (toTarget.sqrMagnitude < radius * radius)
            {
                if (Vector3.Angle(context.transform.forward, toTarget) < angle / 2)
                {
                    context.target = target;
                    Debug.Log(target.name);
                    blackboard.moveToPosition = target.position;
                    return State.Success;
                }
            }
            context.target = null;
            return State.Failure;
        }
    }
}