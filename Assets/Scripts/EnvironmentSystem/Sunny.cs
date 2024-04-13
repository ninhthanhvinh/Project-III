using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : IWeather
{
    WeatherConfigs weatherConfigs;
    public Sunny(WeatherConfigs weatherConfigs)
    {
        this.weatherConfigs = weatherConfigs;
    }

    public void OnWeatherEnter()
    {
        foreach (var modifier in weatherConfigs.Modifiers)
        {
            EnvironmentRunner.instance.AddModifier(modifier);
        }
    }

    public void OnWeatherExit()
    {
        foreach (var modifier in weatherConfigs.Modifiers)
        {
            EnvironmentRunner.instance.RemoveModifier(modifier);
        }
    }

    public void OnWeatherUpdate()
    {
        
    }
}
