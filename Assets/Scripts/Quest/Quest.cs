using RPG.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new();
        [SerializeField] private List<Reward> rewards = new List<Reward>();

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        [System.Serializable]
        public class Reward
        {
            [Min(1)]
            public int number;
            public InventoryItem item;
        }

        [System.Serializable]
        public class Objective
        {
            public string reference;
            public string description;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach (var obj in objectives)
            {
                if (obj.reference.Equals(objectiveRef))
                {
                    return true;
                }
            }
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }
    }

}
