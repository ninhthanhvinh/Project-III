using RPG.Skills;
using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillbook : MonoBehaviour
{
    [SerializeField] SkillbookSlotUI[] skillbookSlots = null;
    public void Unequip(Skill skill)
    {
        foreach (SkillbookSlotUI slot in skillbookSlots)
        {
            if (slot.Skill.Equals(skill))
            {
                slot.IsEquipped = false;
                return;
            }
        }
    }
}
