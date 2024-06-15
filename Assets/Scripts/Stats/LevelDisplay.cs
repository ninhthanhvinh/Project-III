using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        Text levelText;
        float percentage;
        BaseStats baseStats;
        // Start is called before the first frame update
        void Start()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            levelText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            percentage = baseStats.GetLevel();
            levelText.text = percentage.ToString();
        }
    }
}