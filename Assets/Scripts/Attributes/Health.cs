using RPG.Saving;
using RPG.Stats;
using GameDevTV.Utils;
using UnityEngine;
using System;
using BehaviorDesigner.Runtime.Tactical;
using BehaviorDesigner.Runtime;
using Control;
using RPG.Enemy;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable, IDamageable
    {
        [SerializeField] float regenHealthPercentWhileLvlUp = 0f;
        LazyValue<float> healthPoints;
        bool isDead = false;

        public UnityEvent OnDead;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
            OnDead.AddListener(Die);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStats(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(GameObject dmgDealer, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value <= 0)
            {
                AwardExperience(dmgDealer);
                FindObjectOfType<LevelManager>().GetComponent<LevelManager>().OnEnemyDeath(gameObject.GetComponent<Enemies>());            
                OnDead.Invoke();
            }
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            if(TryGetComponent<Animator>(out var anim))
                anim.SetTrigger("die");
            //GetComponent<ActionScheduler>().CancelCurrentAction();
            //GameManager.instance.GetComponent<EnemySpawner>().RemoveEnemy(gameObject);
            if (gameObject.tag != "Player")
            {

                GetComponent<BehaviorTree>().enabled = false;
            }
            else 
            {

                GetComponent<PlayerController>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
            }

            Destroy(gameObject, 5f);
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStats(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStats(Stat.Health));
        }

        private void RegenHealth()
        {
            float regenHP = GetComponent<BaseStats>().GetStats(Stat.Health) * regenHealthPercentWhileLvlUp / 100;
            healthPoints.value = Mathf.Max(healthPoints.value + regenHP, healthPoints.value);
            healthPoints.value = Mathf.Min(healthPoints.value, GetComponent<BaseStats>().GetStats(Stat.Health));
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value == 0)
            {
                OnDead.Invoke();
            }
        }

        public void AwardExperience(GameObject dmgDealer)
        {
            if (!dmgDealer.TryGetComponent<Experience>(out var experience)) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStats(Stat.ExperienceReward));
        }

        public void Damage(float amount)
        {
            TakeDamage(gameObject, amount);
        }

        public bool IsAlive()
        {
            if (healthPoints.value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

