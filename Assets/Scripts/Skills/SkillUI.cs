using Control;
using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] Skill skill;
    [SerializeField] Image icon;
    public Skill Skill { get => skill; }

    private float timer;

    private void Start()
    {
        timer = skill.cooldown;
        icon.sprite = skill.icon;   
        icon.type = Image.Type.Filled;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < skill.cooldown)
        {
            icon.fillAmount = timer / skill.cooldown;
        }
        else
        {
            icon.fillAmount = 1;
        }
    }

    public void UseSkill(PlayerController user)
    {
        if (timer < skill.cooldown) return;
        skill.Use(user);
        timer = 0;
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        icon.sprite = skill.icon;
        icon.type = Image.Type.Filled;
    }
}
