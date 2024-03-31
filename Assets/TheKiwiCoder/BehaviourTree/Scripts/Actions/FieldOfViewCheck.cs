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
            Debug.Log(Vector3.Angle(context.transform.forward, toTarget) < angle / 2);
            if (toTarget.sqrMagnitude < radius * radius)
            {
                if (Vector3.Angle(context.transform.forward, toTarget) < angle / 2)
                {
                    return State.Success;
                }
            }
            return State.Failure;
        }
    }
}