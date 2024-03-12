using RPG.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Inventories
{
    /// <summary>
    ///    /// An slot for the players equipment.
    ///       /// </summary>
    public interface IItemHolder 
    {
        InventoryItem GetItem();
    }  
}


