using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentRunner : MonoBehaviour
{
    public static EnvironmentRunner instance;
    public List<IWeather> weathers;
    public IWeather currentWeather;
    private List<Modifier> currentModifier;
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
    }

    // Update is called once per frame
    void Update()
    {
        currentWeather?.OnWeatherUpdate();
    }

    public void ChangeWeather(IWeather newWeather)
    {
        currentWeather?.OnWeatherExit();
        currentWeather = newWeather;
        currentWeather.OnWeatherEnter();
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
