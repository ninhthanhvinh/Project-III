using RPG.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    /// <summary>
    /// An inventory item that can be placed in the action bar and "Used".
    /// </summary>
    /// <remarks>
    /// This class should be used as a base. Subclasses must implement the `Use`
    /// method.
    /// </remarks>
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
        // CONFIG DATA
        [Tooltip("Does an instance of this item get consumed every time it's used.")]
        [SerializeField] bool consumable = false;

        [SerializeField] private List<Effect> effects;

        // PUBLIC

        /// Trigger the use of this item. Override to provide functionality.
        public virtual void Use(GameObject user)
        {
            foreach (Effect effect in effects)
            {
                effect.ExecuteEffect(user);
            }
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}