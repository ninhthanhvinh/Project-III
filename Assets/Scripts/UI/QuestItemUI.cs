using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Slider progress;

    QuestStatus status;

    public void Setup(QuestStatus questStatus)
    {
        this.status = questStatus;
        title.text = status.GetQuest().GetTitle();
        progress.value = (float) status.GetCompletedCount() / status.GetQuest().GetObjectiveCount();
    }
    public QuestStatus GetQuestStatus()
    {
        return status;
    }
}
