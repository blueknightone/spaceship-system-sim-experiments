/*ComponentWear.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Stores and calculates the wear over time of a component.
Created:  2021-07-29T21:07:03.461Z
Modified: 2021-07-29T23:36:07.989Z
*/

using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public class ComponentWear : Resource
    {
        [Export] private float startingWear;
        [Export] private float WearRatePerSecond;
        [Export] private float WornStateThreshold;
        [Export] private float WornStateMultiplier;
        [Export] private float DamagedStateThreshold;
        [Export] private float DamagedStateMultiplier;
        [Export] private float DestroyedStateThreshold;

        public float CurrentWear { get; private set; }

        /// <summary>
        ///     Updates the current wear value.
        /// </summary>
        /// <param name="amount">Amount to set as current wear level.</param>
        public void SetWearLevel(float amount)
        {
            CurrentWear = amount;
        }

        public ShipComponentState GetWearState(ShipComponentState currentState)
        {
            if ((currentState & ShipComponentState.Inoperable) != 0) return currentState;

            if (CurrentWear >= DestroyedStateThreshold)
            {
                return ShipComponentState.Destroyed;
            }
            else if (CurrentWear >= DamagedStateThreshold)
            {
                return ShipComponentState.Damaged
                        | (
                            currentState
                            & (ShipComponentState.Active | ShipComponentState.Inactive | ShipComponentState.Disabled)
                        );
            }
            else if (CurrentWear >= WornStateThreshold)
            {
                return ShipComponentState.Worn
                        | (
                            currentState
                            & (ShipComponentState.Active | ShipComponentState.Inactive | ShipComponentState.Disabled)
                        );
            }

            return currentState;
        }

        public void AddWear(float amountToAdd)
        {
            CurrentWear += amountToAdd;
        }

        public void OnComponentFunctionTimerTimeout()
        {
            UpdateWear();
        }

         /// <summary>
        ///     Updates the current wear value.
        /// </summary>
        private void UpdateWear() 
        {
            CurrentWear += WearRatePerSecond;
        }
    }
}