using RPG.Enemy;
using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{


    public class SkillUsing : ActionNode
    {
        public int index;
        private SkilledEnemy enemy;
        private Skill skillUsed;
        protected override void OnStart()
        {
            enemy = context.transform.GetComponent<SkilledEnemy>();
            skillUsed = enemy.Skills[index];
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (enemy.CheckCD(skillUsed)) return State.Failure;
            if (context.target == null) return State.Failure;
            enemy.PlayAnimation("Skill 0" + index.ToString());
            enemy.UseSkill(context.target.position, index);
            return State.Success;
        }
    }
}