using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class EnvironmentRunner : MonoBehaviour
{
    public static EnvironmentRunner instance;
    public List<WeatherConfigs> weathers;
    public WeatherConfigs currentWeather;
    private List<Modifier> currentModifier;
    private GameObject player;
    private int weatherIndex;

    public UnityEvent<List<Modifier>> OnWeatherChange;
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
        player = GameObject.FindGameObjectWithTag("Player");
        ChangeWeather(weathers[0]);
        weatherIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentWeather?.OnWeatherUpdate();
    }

    public void ChangeWeather(WeatherConfigs newWeather)
    {

        // Remove current weather modifiers
        if (currentWeather != null)
            foreach (var modifier in currentWeather.Modifiers)
            {
                RemoveModifier(modifier);
            }

        currentWeather = newWeather;
        
        //Add new weather modifiers
        foreach (var modifier in currentWeather.Modifiers)
        {
            AddModifier(modifier);
        }
        //Apply modifiers to entities in map
        OnWeatherChange.Invoke(currentModifier);
        weatherIndex++;
        if (weatherIndex > weathers.Count - 1)
        {
            weatherIndex = 0;
        }

        //Execute effects
        if (currentWeather.Effects != null)
        {
            foreach (var effect in currentWeather.Effects)
            {
                Debug.Log(player);
                effect.ExecuteEffect(player);
            }
        }

        //Instantiate weather prefab
        if (currentWeather.WeatherPrefab != null)
        {
            GameObject weatherPrf = Instantiate(currentWeather.WeatherPrefab, player.transform.position + new Vector3(0f, 20f, 0f), Quaternion.identity);
            StartCoroutine(EndWeather(weatherPrf, currentWeather.Duration));
        }
    }

    public void AddModifier(Modifier modifier)
    {
        currentModifier.Add(modifier);
    }

    public void RemoveModifier(Modifier modifier)
    {
        currentModifier.Remove(modifier);
    }

    private IEnumerator EndWeather(GameObject weather, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(weather);
    }

}