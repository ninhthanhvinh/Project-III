using RPG.Attributes;
using UnityEngine;


namespace RPG.Skills
{
    public class StraightBurstEffect : SkillEffect
    {
        [SerializeField] private LayerMask characterLayer;
        public override void Apply(GameObject owner, Skill skill)
        {
            Collider[] colliders = Physics.OverlapBox(owner.transform.position, new Vector3(2 * skill.range, 4f, skill.range), owner.transform.rotation, characterLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != owner)
                {
                    collider.gameObject.GetComponent<Health>().TakeDamage(owner, skill.GetDamage(owner));
                }
            }
        }
    }
}

