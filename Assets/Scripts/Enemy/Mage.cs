using BehaviorDesigner.Runtime.Tactical;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Enemy
{
    public class Mage : Enemy
    {
        [SerializeField] Animator anim;


        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform spawnPlace;

        NavMeshAgent navMeshAgent;
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnEnable()
        {
            levelManager.OnEnemySpawn(this);
        }
        
        private void OnDestroy()
        {
            levelManager.OnEnemyDeath(this);
        }

        private void Update()
        {
            if (navMeshAgent.velocity.magnitude > Mathf.Epsilon)
            {
                anim.SetBool("isRunning", true);
                SoundManager.instance.PlaySound("run", transform);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        public override void Attack(Vector3 targetPosition)
        {
            anim.SetTrigger("attack");
            Bullet bullet = Instantiate(bulletPrefab, spawnPlace.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Damage = GetComponent<BaseStats>().GetStats(Stat.Damage);
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0;
            bullet.Direction = direction;
            lastAttackTime = Time.time;
        }

    }    
    
}

