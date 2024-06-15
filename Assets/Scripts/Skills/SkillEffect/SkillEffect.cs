using UnityEngine;

namespace RPG.Skills
{
    public abstract class SkillEffect : MonoBehaviour
    {
        protected GameObject target;
        protected GameObject owner;
        public abstract void Apply(GameObject owner, Skill skill);
    }
}