using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnvironmentSystem/WeatherConfigs")]
public class WeatherConfigs : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private Modifier[] modifiers;

    [SerializeField] private GameObject weatherPrefab;
    [SerializeField] private float sunIntensity;

    public float Duration { get => duration; }
    public GameObject WeatherPrefab { get => weatherPrefab; }

    public Modifier[] Modifiers { get => modifiers; }



    public void OnWeatherUpdate()
    {
        FindObjectOfType<DayAndNightControl>().ChangeIntensity(sunIntensity, 10);
    }

}
