using RPG.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class Idle : ActionNode
    {   
        public 
            float time;
        private float timer;
        protected override void OnStart()
        {
            timer = time;
            context.animator.Play("Idle");
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                return State.Success;
            }   
            return State.Running;
        }

    }
}