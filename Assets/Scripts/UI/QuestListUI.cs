using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] Quest[] tempQuests;
    [SerializeField] QuestItemUI questPrefab;

    QuestList questList;

    // Start is called before the first frame update
    void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (QuestStatus status in questList.GetStatuses())
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.Setup(status);
        }
    }
}
