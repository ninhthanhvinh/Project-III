using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] Canvas interactCanvas;
    [SerializeField] AIConversant AIConversant;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactCanvas.gameObject.SetActive(false);
        }
    }
    public void Interact()
    {
        AIConversant.StartDialogue();
    }
}
