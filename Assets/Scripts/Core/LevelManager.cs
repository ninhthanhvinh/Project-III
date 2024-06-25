using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RPG.Enemy
{
    public class LevelManager : MonoBehaviour
    {
        public UnityEvent OnEnemiesCleared;

        [SerializeField] private List<Enemies> enemiesInLevel;

        public void OnEnemySpawn(Enemies enemy)
        {
            enemiesInLevel.Add(enemy);
        }


        public void OnEnemyDeath(Enemies enemy)
        {
            enemiesInLevel.Remove(enemy);
            if (enemiesInLevel.Count == 0)
            {
                OnEnemiesCleared.Invoke();
            }
        }

    }
}