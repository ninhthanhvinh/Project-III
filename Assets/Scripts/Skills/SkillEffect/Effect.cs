using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Skills
{
    public class Effect : ScriptableObject
    {
        [SerializeField] protected Stat statAffected;
        [SerializeField] protected float value;
        public virtual void ExecuteEffect(GameObject user)
        {
            Debug.Log("Effect Executed");
        }
    }    
}

