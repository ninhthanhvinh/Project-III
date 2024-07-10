
using UnityEngine;


namespace RPG.Skills
{
    public class SoundEffect : SkillEffect
    {
        private void Awake()
        {
            Destroy(gameObject, 2f);
        }

        [SerializeField]
        private string soundEffect;
        public override void Apply(GameObject owner, Skill skill)
        {
            SoundManager.instance.PlaySound(soundEffect, owner.transform);
        }
    }

}