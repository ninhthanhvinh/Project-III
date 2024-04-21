using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentRunner : MonoBehaviour
{
    public static EnvironmentRunner instance;
    public List<WeatherConfigs> weathers;
    public WeatherConfigs currentWeather;
    private List<Modifier> currentModifier;

    private int weatherIndex;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentModifier = new();
    }

    private void Start()
    {
        StartCoroutine(ChangeWeather(weathers[0], 0f));
        weatherIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentWeather?.OnWeatherUpdate();
    }

    public IEnumerator ChangeWeather(WeatherConfigs newWeather, float timer)
    {
        yield return new WaitForSeconds(timer);
        if (currentWeather != null)
            foreach (var modifier in currentWeather.Modifiers)
            {
                RemoveModifier(modifier);
            }

        currentWeather = newWeather;
        foreach (var modifier in currentWeather.Modifiers)
        {
            AddModifier(modifier);
        }

        weatherIndex++;
        if (weatherIndex > weathers.Count - 1)
        {
            weatherIndex = 0;
        }

        StartCoroutine(ChangeWeather(weathers[weatherIndex], currentWeather.Duration));
    }

    public void AddModifier(Modifier modifier)
    {
        currentModifier.Add(modifier);
    }

    public void RemoveModifier(Modifier modifier)
    {
        currentModifier.Remove(modifier);
    }
}
