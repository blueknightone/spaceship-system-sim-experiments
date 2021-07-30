/*ShipConsumableStorage.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: A Godot Resource that stores the kind of resource that may be accpeted and how much it will accept.
Created:  2021-07-29T16:37:45.213Z
Modified: 2021-07-30T21:34:48.013Z
*/

using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public partial class ShipConsumableStorage : Resource
    {

        /// <summary>
        ///     Emits when a storage is completely emptied.
        /// </summary>
        [Signal] public delegate void StorageEmptied();
        /// <summary>
        ///     Emits when a storage has filled completely.
        /// </summary>
        /// <param name="overflow">If more of a resource was sent to a storage than it can store, the overflow is returned.</param>
        [Signal] public delegate void StorageFilled(float overflow);

        /// <summary>
        ///     Emits anytime the storage level changes.
        /// </summary>
        /// <param name="currentLevel">Supplies the current amount of stored consumable.</param>
        [Signal] public delegate void StorageLevelChanged(float currentLevel);

        /// <summary>
        ///     Represents what resource is allowed to be stored.
        /// </summary>
        [Export] public ShipConsumableResource AcceptedResource { get; private set; }

        /// <summary>
        ///     Which direction resources from component to system.
        /// </summary>
        [Export] public ComponentFlowDirection FlowDirection { get; private set; }

        /// <summary>
        ///     The amount of resource units currently stored.
        /// </summary>
        [Export] public float CurrentAmountStored { get; private set; }

        /// <summary>
        ///     The maximum number of resource units that can be stored.
        /// </summary>
        [Export] public float MaximumCapacity { get; private set; }

        /// <summary>
        ///     How many units per second the 
        /// </summary>
        [Export] public float BaseUsageRate { get; private set; }

        public float UsageMultiplier { get; set; } = 1f;

        public ShipConsumableStorage()
        {
            AcceptedResource = null;
            CurrentAmountStored = 0f;
            MaximumCapacity = 5600f;
            BaseUsageRate = 1f;
            FlowDirection = ComponentFlowDirection.In;
        }

        public float AddToStorage(float requestedAmount = -1f)
        {
            // If no amount is explicitly provided, then use the base usage rate.
            if (requestedAmount < 0) requestedAmount = BaseUsageRate;
            
            float nextAmount = requestedAmount * UsageMultiplier + CurrentAmountStored;
            float overflow = 0f;

            // If the requested amount is greater than the storage's max capacity,
            // fill the storage and get the overflow amount.
            if (nextAmount > MaximumCapacity)
            {
                overflow = nextAmount - MaximumCapacity;
                CurrentAmountStored = MaximumCapacity;
            }
            // Otherwise, fill the storage by the requested amount
            else
            {
                CurrentAmountStored = nextAmount;
            }

            // Signal the change in levels
            EmitSignal(nameof(StorageLevelChanged), CurrentAmountStored);

            // If the storage is reasonably full, signal that as well.
            if (CurrentAmountStored.Approximately(MaximumCapacity))
            {
                EmitSignal(nameof(StorageFilled));
            }

            // Return the overflow (if any)
            return overflow;
        }

        public float RemoveFromStorage(float requestedAmount = -1f)
        {
            if (requestedAmount < 0f) requestedAmount = BaseUsageRate * UsageMultiplier;

            float requestedAmountUnfulfilled = requestedAmount;

            // If the request is more than what is in the storage, empty the storage and
            // return the amount still needed.
            if (requestedAmount >= CurrentAmountStored)
            {
                requestedAmountUnfulfilled -= CurrentAmountStored;
                CurrentAmountStored = 0f;
                EmitSignal(nameof(StorageLevelChanged), 0f);

            }
            else
            {
                // Otherwise, remove the requested amount from the storage and inform
                // listeners that the storage level has changed and what the new level is.
                CurrentAmountStored -= requestedAmount;
                requestedAmountUnfulfilled = 0f;
                EmitSignal(nameof(StorageLevelChanged), CurrentAmountStored);
            }

            // If the storage is reasonably empty, tell everyone about it.
            if (CurrentAmountStored.Approximately(0f))
            {
                EmitSignal(nameof(StorageEmptied));
            }

            // Return any unfulfilled amount.
            return requestedAmountUnfulfilled;
        }

        public void SetStorageLevel(float value, bool isDecimalPercentage = false)
        {
            float amountToSet;
            if (isDecimalPercentage)
            {
                amountToSet = CurrentAmountStored * value;
            }
            else
            {
                amountToSet = value;
            }

            if (amountToSet > MaximumCapacity)
            {
                CurrentAmountStored = MaximumCapacity;
            }
            else if (amountToSet < 0f)
            {
                CurrentAmountStored = 0f;
            }
            else
            {
                CurrentAmountStored = amountToSet;
            }
        }
    }
}
