using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Skills
{
    public class SkillController : MonoBehaviour
    {
        [SerializeField] SkillUI[] skills;
        public bool Use(int index, PlayerController user)
        {
            if (skills[index] == null) return false;
            skills[index].UseSkill(user);
            return true;
        }
    }
}
