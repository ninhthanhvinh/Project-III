using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.Skills
{
    public class ArrowIndicatorUI : Indicator
    {
        RaycastHit hit;
        Ray ray;

        [SerializeField] Transform targetingPrefab;
        [SerializeField] Camera mainCamera;

        private void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //dir = SetArrowDirection();
           
        }

        private void EndTargeting(PlayerController player, Vector3 vector3)
        {
            player.CanAttack = true;
            Destroy(gameObject);
        }


        public override IEnumerator FindingArea(PlayerController player, Skill skill)
        {
            skill.OnFinish.AddListener(EndTargeting);
            while (true)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Vector3 direction = hit.point - transform.position;
                    Quaternion quaternion = Quaternion.LookRotation(direction);
                    quaternion.eulerAngles = new Vector3(0, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
                    transform.rotation = Quaternion.Lerp(quaternion, transform.rotation, 0);

                    if (Input.GetMouseButtonDown(0))
                    {
                        skill.OnFinish.Invoke(player, direction);
                        break;
                    }
                }
                yield return null;

            }
            gameObject.SetActive(false);
            

        }
    }

}
