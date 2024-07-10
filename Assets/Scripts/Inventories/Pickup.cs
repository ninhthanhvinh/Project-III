using UnityEngine;

namespace RPG.Inventories
{
    
    /// To be placed at the root of a Pickup prefab. Contains the data about the
    /// pickup such as the type of item and the number.
    
    public class Pickup : MonoBehaviour
    {
        // STATE
        InventoryItem item;
        int number;

        // CACHED REFERENCE
        Inventory inventory;

        // LIFECYCLE METHODS

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
        }

        // PUBLIC

        
        /// Set the vital data after creating the prefab.
        
        public void Setup(InventoryItem item, int number)
        {
            this.item = item;
            //TODO

            this.number = number;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            return number;
        }


        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}