using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Skills
{
    public class StraightBurstEffect : SkillEffect
    {
        [SerializeField] private LayerMask characterLayer;
        public override void Apply(GameObject owner, Skill skill)
        {
            Collider[] colliders = Physics.OverlapBox(owner.transform.position, new Vector3(1f, 2f, skill.range), Quaternion.identity, characterLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != owner)
                {
                    collider.gameObject.GetComponent<Health>().TakeDamage(owner, skill.GetDamage());
                }
            }
        }
    }
}

