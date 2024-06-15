using DuloGames.UI;
using UnityEngine;
using UnityEngine.UI;


namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField]
        Text healthText;
        [SerializeField]
        private UIProgressBar healthBar;
        float percentage;
        Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            
        }

        // Update is called once per frame
        void Update()
        {
            percentage = health.GetPercentage();
            healthBar.fillAmount = percentage / 100;
            healthText.text = string.Format(health.GetHealthPoints().ToString() + "/" + health.GetMaxHealthPoints().ToString());
        }
    }
}

