using Control;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace RPG.Skills
{
    
    public class Skill : ScriptableObject
    {
        public string skillName;
        public GameObject indicatorPrefab;
        public GameObject skillVFX;
        public Sprite icon;
        public float cooldown;
        public float range;
        public float damage;
        public string description;
        public UnityEvent<PlayerController, Vector3> OnFinish;
        public TypeOfSkill typeOfSkill;
        public virtual void Use(PlayerController user)
        {
            
        }

        protected void PlayAnimation(PlayerController playerController)
        {
            playerController.PlayAnim(typeOfSkill);
        }
    }
}