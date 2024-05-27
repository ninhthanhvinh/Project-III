using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherPrefabs : MonoBehaviour
{
    public void EndWeather(List<Modifier> list)
    {
        Debug.Log("End Weather");
        Destroy(gameObject);
    }
}
