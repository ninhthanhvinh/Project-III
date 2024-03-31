using Control;
using UnityEngine;
using UnityEngine.Events;
using RPG.Enemy;
using UnityEngine.UI;
using System.Collections;
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

        public virtual void AIUse(SkilledEnemy enemy, Vector3 target) {  }

        public IEnumerator Indicator()
        {

            yield return new WaitForSeconds(1f);   
        }
    }
}