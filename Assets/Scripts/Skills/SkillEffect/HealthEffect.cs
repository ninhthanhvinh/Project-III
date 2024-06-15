using RPG.Attributes;
using UnityEngine;

namespace RPG.Effects
{

    [CreateAssetMenu(fileName = "HealthRegenEffect", menuName = "Skills/Health Regen Effect", order = 0)]
    public class HealthEffect : Effect
    {
        [SerializeField] private float healthRegenAmount;
        public override void ExecuteEffect(GameObject user)
        {
            if (value > 0)
            {
                user.GetComponent<Health>().Heal(healthRegenAmount);
            }
            else
            {
                user.GetComponent<Health>().TakeDamage(null, healthRegenAmount);
            }
        }
    }
}