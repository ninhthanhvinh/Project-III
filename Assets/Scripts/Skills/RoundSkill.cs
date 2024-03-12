using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Skills 
{
    [CreateAssetMenu(menuName = "Skills/RoundSkill")]
    public class RoundSkill : Skill
    {
        public override void Use(PlayerController user)
        {
            user.CanAttack = false;
            GameObject indicator = Instantiate(this.indicatorPrefab, user.transform.position + new Vector3(0, .5f, 0), Quaternion.identity, user.transform);
            RoundIndicator roundIndicator = indicator.GetComponent<RoundIndicator>();
            user.StartCoroutine(roundIndicator.FindingArea(user, this));
        }

        public void SkillEffect(PlayerController playerController, Vector3 vector3)
        {
            PlayAnimation(playerController);
            Transform vfx = Instantiate(skillVFX, playerController.transform.position + new Vector3(0, 1f, 0), Quaternion.identity).transform;
            vfx.position = vector3;
            SkillEffect[] effects = vfx.GetComponentsInChildren<SkillEffect>();

            foreach (SkillEffect effect in effects)
            {
                effect.Apply(playerController.gameObject, this);
            }
        }
    }
}