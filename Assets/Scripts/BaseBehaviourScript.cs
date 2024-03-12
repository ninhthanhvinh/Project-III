using Chronos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviourScript : MonoBehaviour
{
    public Timeline Time
    {
        get
        {
            return GetComponent<Timeline>();
        }
    }
}
