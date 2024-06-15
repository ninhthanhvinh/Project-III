using RPG.Effects;
using UnityEngine;

[CreateAssetMenu(menuName = "EnvironmentSystem/WeatherConfigs")]
public class WeatherConfigs : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private Modifier[] modifiers;
    [SerializeField] private Effect[] effects;

    [SerializeField] private GameObject weatherPrefab;
    [SerializeField] private float sunIntensity;

    public float Duration { get => duration; }
    public GameObject WeatherPrefab { get => weatherPrefab; }

    public Modifier[] Modifiers { get => modifiers; }
    public Effect[] Effects { get => effects; }

    public void OnWeatherUpdate()
    {
        LightAdjustment[] light = FindObjectsOfType<LightAdjustment>();
        foreach (var l in light)
        {
            l.ChangeIntensity(sunIntensity, 10f);
        }
    }
}
