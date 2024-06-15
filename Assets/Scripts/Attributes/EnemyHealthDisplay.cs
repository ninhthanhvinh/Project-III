using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Slider slider;
        float percentage;
        Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponentInParent<Health>();
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            percentage = health.GetPercentage();
            slider.value = percentage / 100;
        }
    }
}
