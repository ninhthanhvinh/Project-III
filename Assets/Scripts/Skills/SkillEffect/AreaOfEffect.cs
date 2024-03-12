using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
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

                    collider.gameObject.GetComponent<Health>().TakeDamage(owner, skill.damage);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }    
}

