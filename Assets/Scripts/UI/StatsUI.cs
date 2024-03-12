using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace RPG.UI
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthText;
        [SerializeField] TextMeshProUGUI damageText;

        private BaseStats baseStats;

        // Start is called before the first frame update
        void Start()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        // Update is called once per frame
        void Update()
        {
            healthText.text = baseStats.GetStats(Stat.Health).ToString();
            damageText.text = baseStats.GetStats(Stat.Damage).ToString();
        }
    }
}