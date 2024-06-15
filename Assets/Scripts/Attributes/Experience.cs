using RPG.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {

        public event Action onExperienceGained;

        private float experiencePoints;
        private float expNeededNextLevel;
        private float expLack;

        public object CaptureState()
        {
            Dictionary<int, float> data = new Dictionary<int, float>();
            data[0] = experiencePoints;
            data[1] = expNeededNextLevel;
            data[2] = expLack;
            return data;
        }

        public float GetPoints()
        {
            return experiencePoints;
        }

        public void ChangeEXPNeed(float EXPNeeded, float EXPLack)
        {
            expNeededNextLevel = EXPNeeded;
            expLack = EXPLack;
        }

        public float GetPercentage()
        {
            return 1 - expLack / expNeededNextLevel;
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public void RestoreState(object state)
        {
            Dictionary<int, float> data = (Dictionary<int, float>)state;
            experiencePoints = data[0];
            expNeededNextLevel = data[1];
            expLack = data[2];
        }
    }
}