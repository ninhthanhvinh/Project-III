using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    GameObject target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = other.gameObject;
        }
        else
            target = null;
    }
    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
