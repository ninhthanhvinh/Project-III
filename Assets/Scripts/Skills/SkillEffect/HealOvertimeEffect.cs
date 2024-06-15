using RPG.Attributes;
using UnityEngine;


namespace RPG.Effects
{


    public class HealOvertimeEffect : OvertimeEffect
    {
        private Health health;
        public float value;

        private void Start()
        {
            health = GetComponent<Health>();
            StartCoroutine(ExecuteEffect());
        }


        protected override void Execute()
        {
            Debug.Log("HealOvertimeEffect");
            if (value > 0)
            {
                health.Heal(value);
            }
            else
                health.TakeDamage(null, -value);
        }
    }
}