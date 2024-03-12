using Control;
using RPG.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField]
    private Canvas rangeCanvas;
    [SerializeField]
    private Canvas targetCanvas;
    [SerializeField] float maxRange = 7f;
    RaycastHit hit;
    Ray ray;


    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
    }

    public IEnumerator FindingArea(PlayerController player, Skill skill)
    {
        targetCanvas.GetComponent<RectTransform>().localScale = Vector3.one * skill.range;
        skill.OnFinish.AddListener(EndTargeting);
        while (true)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 target = new Vector3(hit.point.x, transform.position.y + 0.5f, hit.point.z);
                var distance = Mathf.Min(maxRange, Vector3.Distance(transform.position, target));
                Vector3 newHitPos = transform.position + (target - transform.position).normalized * distance;
                targetCanvas.transform.position = newHitPos;


                if (Input.GetMouseButtonDown(0))
                {
                    skill.OnFinish.Invoke(player, newHitPos);
                    break;
                }
 
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
