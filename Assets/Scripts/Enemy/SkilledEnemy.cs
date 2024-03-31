using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Enemy
{
    public class SkilledEnemy : Enemy
    {
        [SerializeField] private List<Skill> skills;
        [SerializeField] private GameObject projectileNAttack;

        private List<Skill> onCD = new();

        public List<Skill> Skills { get => skills; }

        public void UseSkill(Vector3 target, int index)
        {
            if (skills.Count > 0)
            {
                Skill skillUsed = skills[index];
                skillUsed.AIUse(this, target);
                onCD.Add(skillUsed);
                StartCoroutine(EndCD(skillUsed));
            }
        }

        //public void Attack()
        //{
        //    animator.Play("Attack");
        //}

        public void PerformNAttack()
        {
            Instantiate(projectileNAttack, transform.position, Quaternion.identity);
        }

        private IEnumerator EndCD(Skill skill)
        {
            yield return new WaitForSeconds(skill.cooldown);
            onCD.Remove(skill);
        }

        public bool CheckCD(Skill skill)
        {
            return onCD.Contains(skill);
        }

        public void Idle()
        {
            animator.Play("Idle");
        }

    }
}