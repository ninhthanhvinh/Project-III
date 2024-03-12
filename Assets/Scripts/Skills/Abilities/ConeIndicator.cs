using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConeIndicator : MonoBehaviour
{
    private Canvas canvas;
    private Image coneImage;
    RaycastHit hit;
    Ray ray;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        coneImage = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        SetArrowDirection();
    }

    public void SetArrowDirection()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction = hit.point - transform.position;
            Quaternion quaternion = Quaternion.LookRotation(direction);
            quaternion.eulerAngles = new Vector3(0, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
            canvas.transform.rotation = Quaternion.Lerp(quaternion, canvas.transform.rotation, 0);
        }
    }
}
