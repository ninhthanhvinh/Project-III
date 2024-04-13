using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeather
{
    public void OnWeatherEnter();
    public void OnWeatherExit();
    public void OnWeatherUpdate();
}
