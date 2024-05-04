using BehaviorDesigner.Runtime.Tactical;
using RPG.Attributes;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Enemy
{
    public class Enemy : Enemies, IAttackAgent
    {
        protected Animator animator;

        [SerializeField] private float attackDistance;
        [SerializeField] private float repeatAttackDelay;
        [SerializeField] private float attackAngle;

        protected float lastAttackTime;
        NavMeshAgent navMeshAgent;

        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnEnable()
        {
            levelManager.OnEnemySpawn(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (navMeshAgent.velocity.magnitude > Mathf.Epsilon)
            {
                SoundManager.instance.PlaySound("run", transform);
            }
        }

        public float AttackDistance()
        {
            return attackDistance;
        }

        public bool CanAttack()
        {
            return lastAttackTime + repeatAttackDelay < Time.time;
        }

        public float AttackAngle()
        {
            return attackAngle;
        }

        public void Attack(Vector3 targetPosition)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }

        private void OnTriggerEnter(Collider other)
        {
            float damage = GetComponent<BaseStats>().GetStats(Stat.Damage);
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
                other.GetComponent<Attributes.Health>().TakeDamage(gameObject, damage);
            }
        }

        public void PlayAnimation(string name)
        {
            animator.Play(name);
        }
    }
}


