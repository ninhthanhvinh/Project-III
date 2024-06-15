using RPG.Stats;
using UnityEngine;

namespace RPG.Effects
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

