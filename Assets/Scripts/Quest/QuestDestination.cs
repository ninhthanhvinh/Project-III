using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Quests
{
    public class QuestDestination : QuestCompletion
    {
        [SerializeField]
        private Transform Player;
        [SerializeField]
        private LineRenderer Path;
        [SerializeField]
        private float PathHeightOffset = 1.25f;
        [SerializeField]
        private float SpawnHeightOffset = 1.5f;
        [SerializeField]
        private float PathUpdateSpeed = 0.25f;

        QuestList questList;

        private void Start()
        {
            
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!questList.HasQuest(quest) || questList.GetQuestStatus(quest).IsObjectiveComplete(objective)) return;
            if (other.CompareTag("Player") )
                CompleteObjective();
        }

        public void DrawPath()
        {
            StartCoroutine(DrawPathToCollectable());
        }

        private IEnumerator DrawPathToCollectable()
        {
            WaitForSeconds Wait = new(PathUpdateSpeed);
            NavMeshPath path = new();
            while (transform != null)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(Player.position, out hit, 10.0f, NavMesh.AllAreas))
                {
                    Vector3 playerPos = hit.position;

                    if (NavMesh.CalculatePath(playerPos, transform.position, NavMesh.AllAreas, path))
                    {
                        Path.positionCount = path.corners.Length;

                        for (int i = 0; i < path.corners.Length; i++)
                        {
                            Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                        }
                    }
                    else
                    {
                        Debug.LogError($"Unable to calculate a path on the NavMesh between {Player.position} and {this.transform.position}!");
                    }
                }
                yield return Wait;
            }
        }
    }
}