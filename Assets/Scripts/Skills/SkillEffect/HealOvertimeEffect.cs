using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOvertimeEffect : OvertimeEffect
{
    private Health health;
    public float value;

    private void Start()
    {
        health = GetComponent<Health>();
        StartCoroutine(ExecuteEffect());
    }


    protected override void Execute()
    {
        if (value > 0)
        {
            health.Heal(value);
        }
        else
            health.TakeDamage(null, -value);
    }
}
