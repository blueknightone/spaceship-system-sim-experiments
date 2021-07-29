/*ResourceStorageBuffer.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: A Godot Resource that stores the kind of resource that may be accpeted and how much it will accept.
Created:  2021-07-29T16:37:45.213Z
Modified: 2021-07-29T16:41:06.923Z
*/

using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipResources
{
    public class ResourceStorageBuffer : Resource
    {
        [Signal] public delegate void BufferEmptied();
        [Signal] public delegate void BufferFilled(float overflow);
        [Signal] public delegate void BufferAmountChanged(float currentBufferLevel);

        [Export] public ShipConsumableResource AcceptedResource;
        [Export] public float CurrentAmountStored;
        [Export] public float MaximumCapacity;
        [Export(PropertyHint.ExpRange, "0,100,0.01")] public float LossPercent;

        public ResourceStorageBuffer()
        {
            AcceptedResource = null;
            CurrentAmountStored = 0f;
            MaximumCapacity = 100f;
        }

        public float RemoveFromBuffer(float requestedAmount)
        {
            float requestedAmountUnfulfilled = requestedAmount;

            // If the request is more than what is in the buffer, empty the buffer and
            // return the amount still needed.
            if (requestedAmount >= CurrentAmountStored)
            {
                requestedAmountUnfulfilled -= CurrentAmountStored;
                CurrentAmountStored = 0f;
                EmitSignal(nameof(BufferAmountChanged), 0f);

            }
            else
            {
                // Otherwise, remove the requested amount from the buffer and inform
                // listeners that the buffer level has changed and what the new level is.
                CurrentAmountStored -= requestedAmount;
                requestedAmountUnfulfilled = 0f;
                EmitSignal(nameof(BufferAmountChanged), CurrentAmountStored);
            }

            // If the buffer is reasonably empty, tell everyone about it.
            if (CurrentAmountStored.Approximately(0f))
            {
                EmitSignal(nameof(BufferEmptied));
            }

            // Return any unfulfilled amount.
            return requestedAmountUnfulfilled;
        }

        public float AddToBuffer(float requestedAmount)
        {
            float nextAmount = requestedAmount + CurrentAmountStored;
            float overflow = 0f;

            // If the requested amount is greater than the buffer's max capacity,
            // fill the buffer and get the overflow amount.
            if (nextAmount > MaximumCapacity)
            {
                overflow = nextAmount - MaximumCapacity;
                CurrentAmountStored = MaximumCapacity;
            }
            // Otherwise, fill the buffer by the requested amount
            else
            {
                CurrentAmountStored = nextAmount;
            }

            // Signal the change in levels
            EmitSignal(nameof(BufferAmountChanged), CurrentAmountStored);

            // If the buffer is reasonably full, signal that as well.
            if (CurrentAmountStored.Approximately(MaximumCapacity))
            {
                EmitSignal(nameof(BufferFilled));
            }

            // Return the overflow (if any)
            return overflow;
        }
    }
}
