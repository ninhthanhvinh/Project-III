using Control;
using RPG.Effects;
using RPG.Enemy;
using UnityEngine;

namespace RPG.Skills
{
    [CreateAssetMenu(fileName = "ArrowIndicatorSkill", menuName = "Skills/ArrowIndicatorSkill")]
    public class ArrowIndicatorSkill : Skill
    {
        private void Awake()
        {
            OnFinish.AddListener(SkillEffect);
        }

        public override void Use(PlayerController user)
        {
            user.CanAttack = false;
            GameObject indicator = Instantiate(this.indicatorPrefab, 
                user.transform.position + new Vector3(0, 2f, 0), Quaternion.identity, user.transform);
            ArrowIndicatorUI arrowIndicatorUI = indicator.GetComponent<ArrowIndicatorUI>();
            user.StartCoroutine(arrowIndicatorUI.FindingArea(user, this));
        }

        public void SkillEffect(PlayerController playerController, Vector3 vector3)
        {
            playerController.transform.LookAt(vector3);
            PlayAnimation(playerController);
            SoundManager.instance.PlaySound("slash", playerController.transform);
            Transform vfx = Instantiate(skillVFX, playerController.transform.position 
                + new Vector3(0, 1f, 0), Quaternion.identity).transform;
            vfx.rotation = Quaternion.LookRotation(vector3);

            SkillEffect[] effects = vfx.GetComponentsInChildren<SkillEffect>();

            foreach (SkillEffect effect in effects)
            {
                effect.Apply(playerController.gameObject, this);
            }

        }

        public override void AIUse(SkilledEnemy enemy, Vector3 target)
        {
            Transform vfx = Instantiate(skillVFX, target, Quaternion.identity).transform;
            vfx.rotation = Quaternion.LookRotation(target);

            SkillEffect[] effects = vfx.GetComponentsInChildren<SkillEffect>();
            foreach (SkillEffect effect in effects)
            {
                effect.Apply(enemy.gameObject, this);
            }

        }
    }
    
}
