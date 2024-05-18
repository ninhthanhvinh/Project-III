using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjustment : MonoBehaviour
{
    private Light lightAdjust;

    private void Awake()
    {
        lightAdjust = GetComponent<Light>();
    }

    public void ChangeIntensity(float intensity, float duration)
    {
        lightAdjust.intensity = Mathf.Lerp(lightAdjust.intensity, intensity, duration * Time.deltaTime);
    }
}
