using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName = "NPC";
        [SerializeField] GameObject player = null;

        public Dialogue SetDialogue { set => dialogue = value; }

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public string GetConversantName()
        {
            return conversantName;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this ,dialogue);
            }
            return true;
        }

        public void StartDialogue()
        {
            player.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
        }
    }
}

