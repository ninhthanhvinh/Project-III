using RPG.Saving;
using RPG.Stats;
using GameDevTV.Utils;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;
using BehaviorDesigner.Runtime;
using Control;
using RPG.Enemy;
using UnityEngine.Events;
using TheKiwiCoder;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable, IDamageable
    {
        [SerializeField] float regenHealthPercentWhileLvlUp = 0f;
        LazyValue<float> healthPoints;
        LazyValue<float> defense;
        bool isDead = false;

        const float DEFENCE_AFFECT_ON_DAMAGE = 0.25f;

        public UnityEvent OnDead;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
            defense = new LazyValue<float>(GetInitialDefense);
            OnDead.AddListener(Die);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStats(Stat.Health);
        }

        private float GetInitialDefense()
        {
            return GetComponent<BaseStats>().GetStats(Stat.Defense);
        }

        private void Start()
        {
            healthPoints.ForceInit();
            defense.ForceInit();
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
            healthPoints.value = Mathf.Max(healthPoints.value + defense.value * DEFENCE_AFFECT_ON_DAMAGE - damage, 0);
            if (TryGetComponent<PlayerController> (out var playerController))
            {
                StartCoroutine(playerController.Damaged());            
            }
            DamagePopUpGenerator.current.CreatePopUp(transform.position + new Vector3(0f, 1f, 0.5f), damage.ToString(), Color.red);
            if (healthPoints.value <= 0)
            {
                if (dmgDealer == null)
                {
                    return;
                }
                OnDead.Invoke();
                FindObjectOfType<LevelManager>().GetComponent<LevelManager>().OnEnemyDeath(gameObject.GetComponent<Enemies>());            
                AwardExperience(dmgDealer);
            }
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            if(TryGetComponent<Animator>(out var anim))
                anim.SetTrigger("die");
            else if(GetComponentInChildren<Animator>() != null)
                GetComponentInChildren<Animator>().SetTrigger("die");
            //GetComponent<ActionScheduler>().CancelCurrentAction();
            //GameManager.instance.GetComponent<EnemySpawner>().RemoveEnemy(gameObject);
            if (!gameObject.CompareTag("Player"))
            {

                GetComponent<BehaviourTreeRunner>().enabled = false;
                
            }
            else 
            {
                
                GetComponent<PlayerController>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                GameManager.instance.OnLose.Invoke();
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

        public void Heal(float healthToRestore)
        {
            DamagePopUpGenerator.current.CreatePopUp(transform.position + new Vector3(0f, 1f, 0.5f), healthToRestore.ToString(), Color.green);
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetComponent<BaseStats>().GetStats(Stat.Health));
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
            if (!dmgDealer.TryGetComponent<Experience>(out var experience) || dmgDealer == gameObject) return;
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

