using RPG.Inventories;
using RPG.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        List<QuestStatus> statuses = new List<QuestStatus>();

        public event Action onUpdate;

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            onUpdate?.Invoke();
        }

        public bool CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if (status.IsComplete())
            {
                GiveRewards(quest);
            }
            onUpdate?.Invoke();
            return true;
        }

        private void GiveRewards(Quest quest)
        {
            foreach (Quest.Reward reward in quest.GetRewards())
            {
                bool success = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
                if (!success)
                {
                    GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                }
            }
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        public QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        public object CaptureState()
        {
            List<object> states = new();
            foreach (QuestStatus status in statuses)
            {
                states.Add(status.CaptureState());
            }

            return states;
        }

        public void RestoreState(object state)
        {
            
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if (predicate == "HasQuest") return null;
            return HasQuest(Quest.GetByName(parameters[0]));
        }
    }

}
