using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeEffect : MonoBehaviour
{
    private readonly float duration = 5f;
    private Health health;
    private float timer;
    const float tickRate = 1f;

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
            health.TakeDamage(this.gameObject, 100);
            
        }
        timer -= Time.deltaTime;
    }

    private IEnumerator ExecuteEffect()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this);
    }
}
