using UnityEngine;
using RPG.Saving;

namespace RPG.Inventories
{

    /// Spawns pickups that should exist on first load in a level. This
    /// automatically spawns the correct prefab for a given inventory item.

    public class PickupSpawner : MonoBehaviour, ISaveable
    {
        // CONFIG DATA
        [SerializeField] InventoryItem item = null;
        [SerializeField] int number = 1;

        //TODO

        // LIFECYCLE METHODS
        private void Awake()
        {
            // Spawn in Awake so can be destroyed by save system after.
            SpawnPickup();
        }

        // PUBLIC

    
        /// Returns the pickup spawned by this class if it exists.

        public Pickup GetPickup() 
        { 
            return GetComponentInChildren<Pickup>();
        }

    
        /// True if the pickup was collected.
    
        public bool isCollected() 
        { 
            return GetPickup() == null;
        }

        //PRIVATE

        private void SpawnPickup()
        {
            // TODO
            var spawnedPickup = item.SpawnPickup(transform.position, number);
            spawnedPickup.transform.SetParent(transform);
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }

        object ISaveable.CaptureState()
        {
            return isCollected();
        }

        void ISaveable.RestoreState(object state)
        {
            bool shouldBeCollected = (bool)state;

            if (shouldBeCollected && !isCollected())
            {
                DestroyPickup();
            }

            if (!shouldBeCollected && isCollected())
            {
                SpawnPickup();
            }
        }
    }
}