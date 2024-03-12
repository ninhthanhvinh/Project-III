using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Skills
{
    public class SkillItem : ScriptableObject
    {
        public string skillName;
        public float skillCooldown;
        public float skillRange;
        public float skillDamage;
        public GameObject skillIndicator;
        public GameObject skillPrefab;

        public virtual bool UseSkill()
        {
            
            return true;
        }
    }
}

