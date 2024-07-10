using DuloGames.UI;
using RPG.Skills;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Enemy
{
    public class SkilledEnemy : Enemy
    {
        [SerializeField] private List<Skill> skills;
        [SerializeField] private GameObject projectileNAttack;
        [SerializeField] private Transform projectileSpawnPoint;

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

        public override void Attack(Vector3 targetPosition)
        {
            animator.Play("Attack");
            float dmg = GetComponent<BaseStats>().GetStats(Stat.Damage);
            StartCoroutine(PerformNAttack(targetPosition, dmg, 1.7f / 4));
        }

        public IEnumerator PerformNAttack(Vector3 target, float dmg, float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            Bullet bullet = Instantiate(projectileNAttack, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Direction = (target - transform.position).normalized;
            bullet.Damage = dmg;
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