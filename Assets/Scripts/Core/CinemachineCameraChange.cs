using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> listCamera;

    private int currentCameraIndex = 0;
    private Transform player;
    // Start is called before the first frame update

    const float timeToReChange = 10f;
    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {   
        if(player == null)
        {
            return;
        }
        Vector3 direction = player .position - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, LayerMask.GetMask("Default"));
        if (Input.GetKeyDown(KeyCode.M) || hit.collider != null && !hit.collider.CompareTag("Player") && timer <= 0f)
        {
            Debug.Log("Change Camera");
            timer = timeToReChange;
            ChangeCamera(GetNextIndex());
        }

        timer -= Time.deltaTime;
    }

    private void ChangeCamera(int index)
    {
        for (int i = 0; i < listCamera.Count; i++)
        {
            if (i == index)
            {
                listCamera[i].SetActive(true);
            }
            else
            {
                listCamera[i].SetActive(false);
            }
        }

        currentCameraIndex = index;
    }

    private int GetNextIndex()
    {
        if (currentCameraIndex + 1 >= listCamera.Count)
        {
            return 0;
        }
        else
        {
            return currentCameraIndex + 1;
        }
    }
}
