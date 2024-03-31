using Control;
using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundIndicator : Indicator
{
    [SerializeField]
    private Canvas rangeCanvas;
    [SerializeField] float maxRange = 7f;


    public override IEnumerator FindingArea(PlayerController player, Skill skill)
    {
        rangeCanvas.GetComponent<RectTransform>().localScale = Vector3.one * skill.range / 2;
        skill.OnFinish.AddListener(EndTargeting);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                skill.OnFinish.Invoke(player, transform.position);
                break;
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void EndTargeting(PlayerController player, Vector3 arg1)
    {
        player.CanAttack = true;
        Destroy(gameObject);
    }
}
