
using Control;
using RPG.Enemy;
using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Skills/TargetRangeSkill")]
public class TargetRangeSkill : Skill
{
    [SerializeField] Vector3 offsetInstantiate;
    private void Awake()
    {
        OnFinish.AddListener(SkillEffect);
    }

    public override void Use(PlayerController user)
    {
        user.CanAttack = false;
        GameObject indicator = Instantiate(this.indicatorPrefab, user.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity, user.transform);
        TargetIndicator targetIndicator = indicator.GetComponent<TargetIndicator>();
        user.StartCoroutine(targetIndicator.FindingArea(user, this));
    }

    public void SkillEffect(PlayerController playerController, Vector3 vector3)
    {
        PlayAnimation(playerController);
        Transform vfx = Instantiate(skillVFX, playerController.transform.position + new Vector3(0, 1f, 0), skillVFX.transform.rotation).transform;
        vfx.position = vector3 + offsetInstantiate;
        SkillEffect[] effects = vfx.GetComponentsInChildren<SkillEffect>();

        foreach (SkillEffect effect in effects)
        {
            effect.Apply(playerController.gameObject, this);
        }
    }

    public override void AIUse(SkilledEnemy enemy, Vector3 target)
    {
        Transform vfx = Instantiate(skillVFX, target, Quaternion.identity).transform;

        SkillEffect[] effects = vfx.GetComponentsInChildren<SkillEffect>();
        foreach (SkillEffect effect in effects)
        {
            effect.Apply(enemy.gameObject, this);
        }

    }
}
