using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;
        public UnityEvent OnQuestAdd;
        [SerializeField] private QuestDestination destination;

        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            OnQuestAdd.Invoke();
            if (destination != null)
            {
                Debug.Log("Drawing path to collectable");
                destination.DrawPath();
            }

            questList.AddQuest(quest);
            
        }

    }

}