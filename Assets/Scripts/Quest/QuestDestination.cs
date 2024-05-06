using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestDestination : QuestCompletion
    {
        private void OnTriggerEnter(Collider other)
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            if (!questList.HasQuest(quest)) return;
            if (other.CompareTag("Player") )
                CompleteObjective();
        }
    }
}