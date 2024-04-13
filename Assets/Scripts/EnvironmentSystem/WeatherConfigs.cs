using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnvironmentSystem/WeatherConfigs")]
public class WeatherConfigs : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private Modifier[] modifiers;

    public Modifier[] Modifiers { get => modifiers; }
}
