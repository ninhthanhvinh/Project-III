using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Enemy
{
    public class Necromancer : MonoBehaviour, IAttackAgent
    {
        Animator animator;

        [SerializeField] private float attackDistance;
        [SerializeField] private float repeatAttackDelay;
        [SerializeField] private float attackAngle;

        protected float lastAttackTime;

        private List<Enemies> summonedMonster;

        public void Attack(Vector3 targetPosition)
        {
            //if()

        }

        public float AttackAngle()
        {
            return attackAngle;
        }

        public float AttackDistance()
        {
            return attackDistance;
        }

        public bool CanAttack()
        {
            return lastAttackTime + repeatAttackDelay < Time.time;
        }

    }
}