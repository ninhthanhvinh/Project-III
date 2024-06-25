
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName = "NPC";
        [SerializeField] GameObject player = null;

        public Dialogue SetDialogue { set => dialogue = value; }

        public string GetConversantName()
        {
            return conversantName;
        }

        public void StartDialogue()
        {
            player.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
        }

        public void ChangeDialogue(Dialogue dialogue)
        {
            this.dialogue = dialogue;
        }
    }
}

