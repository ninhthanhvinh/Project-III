using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Effects
{

    public class WeatherChangeEffect : OvertimeEffect
    {
        private WeatherConfigs weather;
        private EnvironmentRunner environmentRunner;
        private WeatherConfigs previousWeather;
        public void SetWeather(WeatherConfigs weather, float duration)
        {
            this.weather = weather;
            this.duration = duration;
        }

        private void Awake()
        {
            environmentRunner = FindObjectOfType<EnvironmentRunner>();
            previousWeather = environmentRunner.currentWeather;
        }

        private void Start()
        {
            StartCoroutine(ExecuteEffect());
            environmentRunner.ChangeWeather(weather);
        }

        protected override IEnumerator ExecuteEffect()
        {
            yield return new WaitForSeconds(duration);
            environmentRunner.ChangeWeather(previousWeather);
            Destroy(this);
        }
    }
}