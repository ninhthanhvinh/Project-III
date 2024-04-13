using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour
{
    private BaseStats stats;
    [SerializeField] private Stat statDisplayed;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = stats.GetStats(statDisplayed).ToString();
    }
}
