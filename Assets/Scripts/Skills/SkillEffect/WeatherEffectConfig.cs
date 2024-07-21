using RPG.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherChange", menuName = "Skills/Effect/WeatherChange")]
public class WeatherEffectConfig : Effect
{
    [SerializeField]
    private WeatherConfigs weather;
    [SerializeField]
    private float duration = 20f;

    public override void ExecuteEffect(GameObject user)
    {
        WeatherChangeEffect fx = user.AddComponent<WeatherChangeEffect>();
        Debug.Log(user + " " +  fx.name);
        fx.SetWeather(weather, duration);
    }
}