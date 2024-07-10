using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentRunner : MonoBehaviour
{
    public static EnvironmentRunner instance;
    public List<WeatherConfigs> weathers;
    public WeatherConfigs currentWeather;
    private List<Modifier> currentModifier;
    private GameObject player;
    private int weatherIndex;

    float count;

    public UnityEvent<List<Modifier>> OnWeatherChange;
    private float updateWeatherDuration = 20f;

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
        if (count > updateWeatherDuration)
        {
            UpdateWeather();
            count = 0;
        }
        count += Time.deltaTime;
    }

    private void UpdateWeather()
    {
        WeatherPrefabs prefab = FindObjectOfType<WeatherPrefabs>();
        if (prefab != null)
        {
            prefab.gameObject.transform.position = player.transform.position
                + new Vector3(0f, 20f, 0f);
            if (currentWeather.Effects != null)
            {
                foreach (var effect in currentWeather.Effects)
                {
                    effect.ExecuteEffect(player);
                }
            }
        }
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
        updateWeatherDuration = currentWeather.Duration;
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
                effect.ExecuteEffect(player);
            }
        }

        //Instantiate weather prefab
        if (currentWeather.WeatherPrefab != null)
        {
            WeatherPrefabs weatherPrf = Instantiate(currentWeather.WeatherPrefab, player.transform.position
                + new Vector3(0f, 20f, 0f), Quaternion.identity).GetComponent<WeatherPrefabs>();
            OnWeatherChange.AddListener(weatherPrf.EndWeather);
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