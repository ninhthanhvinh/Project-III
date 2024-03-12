using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Skills
{
    public class SoundEffect : SkillEffect
    {
        [SerializeField]
        private string soundEffect;
        public override void Apply(GameObject owner, Skill skill)
        {
            SoundManager.instance.PlaySound(soundEffect, owner.transform);
        }
    }

}