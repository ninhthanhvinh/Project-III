using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Skills;

[CreateAssetMenu(fileName = "OTEffectConfig", menuName = "Skills/Effect/OTEffectConfig")]
public class OTEffectConfig : Effect
{
    public float duration = 5f;
    public override void ExecuteEffect(GameObject user)
    {
        HealOvertimeEffect effect = user.AddComponent<HealOvertimeEffect>();
        effect.value = value;
        effect.SetDuration(duration);
    }
}
