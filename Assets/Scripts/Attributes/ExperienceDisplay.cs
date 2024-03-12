using DuloGames.UI;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField]
        Text experienceText;
        [SerializeField]
        private UIProgressBar experienceBar;
        float percentage;
        Experience experience;
        // Start is called before the first frame update
        void Start()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();

        }

        // Update is called once per frame
        void Update()
        {
            percentage = 100 * experience.GetPercentage();
            experienceBar.fillAmount = percentage / 100;
            experienceText.text = percentage.ToString() + "%";
        }
    }
}
