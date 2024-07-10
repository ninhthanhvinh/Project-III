using System;
using UnityEngine;
using RPG.Saving;

namespace RPG.Inventories
{
    /// <summary>
    /// Provides storage for the player inventory. A configurable number of
    /// slots are available.
    ///
    /// This component should be placed on the GameObject tagged "Player".
    /// </summary>
    public class Inventory : MonoBehaviour, ISaveable
    {
        
        [Tooltip("Allowed size")]
        [SerializeField] int inventorySize = 16;

        
        InventorySlot[] slots;

        public struct InventorySlot
        {
            public InventoryItem item;
            public int number;
        }

        


       
        public event Action inventoryUpdated;

        
        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        
        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        
        public int GetSize()
        {
            return slots.Length;
        }

        
        public bool AddToFirstEmptySlot(InventoryItem item, int number)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i].item = item;
            slots[i].number += number;
            inventoryUpdated?.Invoke();
            return true;
        }

        
        /// Is there an instance of the item in the inventory?
        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].item, item))
                {
                    return true;
                }
            }
            return false;
        }

        /// Return the item type in the given slot.
        public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot].item;
        }

        public int GetNumberInSlot(int index)
        {
            return slots[index].number;
        }

        /// Remove the item from the given slot.
        public void RemoveFromSlot(int slot, int number)
        {
            slots[slot].number -= number;
            if (slots[slot].number <= 0)
            {
                slots[slot].number = 0;
                slots[slot].item = null;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }


        /// Will add an item to the given slot if possible. If there is already
        /// a stack of this type, it will add to the existing stack. Otherwise,
        /// it will be added to the first empty slot.
        public bool AddItemToSlot(int slot, InventoryItem item, int number)
        {
            if (slots[slot].item != null)
            {
                return AddToFirstEmptySlot(item, number); ;
            }

            var i = FindStack(item);
            if (i >= 0)
            {
                slot = i;
            }

            slots[slot].item = item;
            slots[slot].number += number;

            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        // PRIVATE

        private void Awake()
        {
            slots = new InventorySlot[inventorySize];
        }



        /// Find a slot that can accomodate the given item.
        private int FindSlot(InventoryItem item)
        {
            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }

            return i;
        }


        /// Find an empty slot.
        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    return i;
                }
            }
            return -1;
        }


        /// Find an existing stack of this item type.
        private int FindStack(InventoryItem item)
        {
            if (!item.IsStackable())
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].item, item))
                {
                    return i;
                }
            }
            return -1;
        }

        [System.Serializable]
        private struct InventorySlotRecord
        {
            public string itemID;
            public int number;
        }


        object ISaveable.CaptureState()
        {
            var slotRecords = new InventorySlotRecord[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                if (slots[i].item != null)
                {
                    slotRecords[i].itemID = slots[i].item.GetItemID();
                    slotRecords[i].number = slots[i].number;
                }
            }
            return slotRecords;
        }

        void ISaveable.RestoreState(object state)
        {
            var slotRecords = (InventorySlotRecord[])state;
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i].item = InventoryItem.GetFromID(slotRecords[i].itemID);
                slots[i].number = slotRecords[i].number;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
    }
}