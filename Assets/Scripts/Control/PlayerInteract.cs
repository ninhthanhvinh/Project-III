using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    float interactDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) GetCloseNPC(); 
    }

    private void GetCloseNPC()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<NPCInteractable>(out NPCInteractable npc))
            {
                npc.Interact();
            }
        }
    }   
}
