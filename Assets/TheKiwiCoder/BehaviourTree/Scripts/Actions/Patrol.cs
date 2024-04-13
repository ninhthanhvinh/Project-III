using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class Patrol : ActionNode
    {
        const float PATROL_DISTANCE = 10.0f;
        public float speed = 5;
        public float stoppingDistance = 0.1f;
        public bool updateRotation = true;
        public float acceleration = 40.0f;
        public float tolerance = 1.0f;
        protected override void OnStart()
        {
            context.agent.stoppingDistance = stoppingDistance;
            context.agent.speed = speed;
            context.agent.destination = GenerateRandomPoint();
            context.agent.updateRotation = updateRotation;
            context.agent.acceleration = acceleration;
            context.animator.Play("Run");
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (context.agent.pathPending)
            {
                return State.Running;
            }

            if (context.agent.remainingDistance < tolerance)
            {
                return State.Success;
            }

            if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
            {
                return State.Failure;
            }

            return State.Running;
        }

        private Vector3 GenerateRandomPoint()
        {
            float patrolX = Random.Range(-PATROL_DISTANCE, PATROL_DISTANCE);
            float patrolZ = Random.Range(-PATROL_DISTANCE, PATROL_DISTANCE);
            Vector3 randomPoint = new Vector3(patrolX, 0, patrolZ) + context.transform.position; 
            return randomPoint;
        }
    }
}