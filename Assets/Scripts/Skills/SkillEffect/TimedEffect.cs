using RPG.Skills;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TimedEffect", menuName = "Skills/Timed Effect", order = 0)]
public class TimedEffect : Effect
{
    public float duration = 10f;
    public override void ExecuteEffect(GameObject user)
    {
        user.GetComponent<BuffHolder>().AddBuff(statAffected, value, duration);
        
    }

}
