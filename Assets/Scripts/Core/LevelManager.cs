using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal.Internal;
namespace RPG.Enemy
{
    public class LevelManager : MonoBehaviour
    {
        private static bool firstInstance = true;
        private void Awake()
        {
            InitScene.AddListener(() =>
            {
                firstInstance = false;
            });
            if (firstInstance)
            {
                InitScene.Invoke();
            }
        }

        public UnityEvent InitScene;

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