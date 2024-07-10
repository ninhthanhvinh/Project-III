using Cinemachine;
using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = FindObjectOfType<PlayerController> ().gameObject;
        cam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
