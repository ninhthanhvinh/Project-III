using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    float _scaleFactor = 50f;
    [SerializeField] private bool isScale = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        if (isScale)
            Scale();
    }

    

    private void Scale()
    {
        if (mainCamera)
        {
            float camHeight;
            if (mainCamera.orthographic)
            {
                camHeight = mainCamera.orthographicSize * 2;
            }
            else
            {
                float distanceToCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
                camHeight = 2.0f * distanceToCamera * Mathf.Tan(Mathf.Deg2Rad * (mainCamera.fieldOfView * 0.5f));
            }
            float scale = (camHeight / Screen.width) * _scaleFactor;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
