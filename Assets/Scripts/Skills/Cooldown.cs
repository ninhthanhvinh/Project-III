using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer =+ Time.deltaTime;
    }

    public bool IsReady()
    {
        return timer > cooldown;
    }
}
