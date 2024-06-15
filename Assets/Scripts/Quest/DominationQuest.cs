using RPG.Attributes;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Quests
{
    public class DominationQuest : QuestCompletion
    {
        [SerializeField] private List<GameObject> target;
        private int count;
        private void Start()
        {
            count = target.Count;
            foreach (var t in target)
            {
                var health = t.GetComponent<Health>();
                health.OnDead.AddListener(OnTargetDeath);
            }
        }

        private void OnTargetDeath()
        {
            count --;
            if (count <= 0)
                CompleteObjective();
        }
    }
}