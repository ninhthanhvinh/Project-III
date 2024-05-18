using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeEffect : MonoBehaviour
{

    protected float duration = 5f;

    private float timer;
    const float tickRate = 1f;

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            timer = tickRate;
            Execute();
        }
        timer -= Time.deltaTime;
    }
    
    protected virtual void Execute()
    {
        Debug.Log("Overtime effect executed");
    }

    protected virtual IEnumerator ExecuteEffect()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this);
    }
}
