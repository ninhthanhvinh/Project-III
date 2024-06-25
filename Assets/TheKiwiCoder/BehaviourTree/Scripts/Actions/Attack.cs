using RPG.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public class Attack : ActionNode
    {
        Enemy enemy;
        protected override void OnStart()
        {
            enemy = context.transform.GetComponent<Enemy>();
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (enemy.CanAttack())
            {
                enemy.Attack(context.target.position);
                return State.Success;
            }
            return State.Failure;
        }
    }
}