using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHolder : MonoBehaviour, IModifierProvider
{
    [SerializeField] Modifier[] additiveModifiers;
    [SerializeField] Modifier[] percentageModifiers;

    public IEnumerable<float> GetAdditiveModifiers(Stat stat)
    {
        foreach (var modifier in additiveModifiers)
        {
            if (modifier.stat == stat)
            {
                yield return modifier.value;
            }
        }
    }

    public IEnumerable<float> GetPercentageModifiers(Stat stat)
    {
        foreach (var modifier in percentageModifiers)
        {
            if (modifier.stat == stat)
            {
                yield return modifier.value;
            }
        }
    }

    public void AddBuff(Stat stat, float value, float duration)
    {
        foreach (var modifier in additiveModifiers)
        {
            if (modifier.stat == stat)
            {
                modifier.AddValue(value);
                Invoke("EndBuff", duration);
                return;
            }
        }
    }

    public void EndBuff(Stat stat, float value)
    {
        foreach (var modifier in additiveModifiers)
        {
            if (modifier.stat == stat)
            {
                modifier.AddValue(-value);
                return;
            }
        }
    }
}
