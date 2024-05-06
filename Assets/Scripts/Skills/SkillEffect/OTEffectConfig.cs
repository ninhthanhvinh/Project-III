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
        OvertimeEffect effect = user.AddComponent<OvertimeEffect>();
        effect.value = value;
        effect.SetDuration(duration);
    }
}
