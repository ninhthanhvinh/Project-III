using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SkillbookSlotUI : MonoBehaviour
    {
        [SerializeField] Image icon = null;
        [SerializeField] TextMeshProUGUI skillName = null;
        [SerializeField] Skill skill = null;
        [SerializeField] SkillUI[] equipablePositions;

        Skillbook skillbook;
        [SerializeField]
        private bool isEquipped = false;

        public Skill Skill { get { return skill; } }
        public bool IsEquipped { get { return isEquipped; } set { isEquipped = value; } }

        private void Start()
        {
            icon.sprite = skill.icon;
            skillName.text = skill.name;
            skillbook = GetComponentInParent<Skillbook>();
        }

        public void Equip(int position)
        {
            if (isEquipped)
            {
                return;
            }
            if (equipablePositions[position].Skill != null)
            {
                skillbook.Unequip(equipablePositions[position].Skill);
            }
            isEquipped = true;
            equipablePositions[position].SetSkill(skill);
        }
    }
}