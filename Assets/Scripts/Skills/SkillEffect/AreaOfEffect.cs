using RPG.Attributes;
using UnityEngine;

namespace RPG.Skills
{
    public class AreaOfEffect : SkillEffect
    {
        [SerializeField] private LayerMask characterLayer;
        public override void Apply(GameObject owner, Skill skill)
        {
            this.owner = owner;

            Collider[] colliders = Physics.OverlapSphere(transform.position, skill.range, characterLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != owner)
                {
                    Debug.Log("Applying damage to " + collider.gameObject.name);
                    collider.gameObject.GetComponent<Health>().TakeDamage(owner, skill.GetDamage(owner));
                }
            }
        }
        public GameObject GetOwner()
        {
            return owner;
        }
    }    
}

