using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] Quest quest;
        [SerializeField] string objective;
        [SerializeField] private GameObject notiUI;
        public UnityEvent OnComplete;

        public void CompleteObjective()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            if (!questList.HasQuest(quest)) return;
            questList.CompleteObjective(quest, objective);
            notiUI.SetActive(true);
            notiUI.GetComponent<NotiUI>().ShowNoti("Objective Completed!");
            OnComplete.Invoke();
        }
    }
} 