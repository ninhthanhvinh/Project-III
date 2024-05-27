using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChangeCreature : MonoBehaviour
{
    [SerializeField]
    private List<WeatherConfigs> WeatherConfigs = new();
    
    public void ChangeWeather(WeatherConfigs weatherConfigs)
    {
        FindAnyObjectByType<EnvironmentRunner>().ChangeWeather(weatherConfigs);
    }
}
