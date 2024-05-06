using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeEffect : MonoBehaviour
{
    private float duration = 5f;
    private Health health;
    public float value;
    private float timer;
    const float tickRate = 1f;

    public void SetDuration(float duration) => this.duration = duration;

    private void Start()
    {
        health = GetComponent<Health>();
        StartCoroutine(ExecuteEffect());
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            timer = tickRate;
            if (value > 0)
            {
                health.Heal(value);
            }
            else
                health.TakeDamage(this.gameObject, -value);
        }
        timer -= Time.deltaTime;
    }

    private IEnumerator ExecuteEffect()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this);
    }
}
