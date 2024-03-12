using RPG.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class RunoverPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.tag == "Player")
            {
                Pickup pickup = GetComponent<Pickup>();
                if (pickup)
                {
                    pickup.PickupItem();
                }
            }
        }
    }
}