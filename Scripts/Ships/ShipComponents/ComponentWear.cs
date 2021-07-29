/*ComponentWear.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Stores and calculates the wear over time of a component.
Created:  2021-07-29T21:07:03.461Z
Modified: 2021-07-29T21:07:20.552Z
*/

using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public class ComponentWear : Timer
    {
        [Export] private float startingWear;
        [Export] private float WearRatePerSecond;
        [Export] private float WornStateThreshold;
        [Export] private float DamagedStateThreshold;
        [Export] private float DestroyedStateThreshold;

        public float GetCurrentWear => currentWear;
        private float currentWear;

        /// <summary>
        ///     Updates the current wear value.
        /// </summary>
        public void ApplyWear() 
        {
            currentWear += WearRatePerSecond;
        }

        public ShipComponentState GetWearState(ShipComponentState currentState)
        {
            if ((currentState & ShipComponentState.Inoperable) != 0) return currentState;

            if (currentWear >= DestroyedStateThreshold)
            {
                return ShipComponentState.Destroyed;
            }
            else if (currentWear >= DamagedStateThreshold)
            {
                return ShipComponentState.Damaged 
                        | (
                            currentState 
                            & (ShipComponentState.Active | ShipComponentState.Inactive | ShipComponentState.Disabled)
                        );
            }
            else if (currentWear >= WornStateThreshold)
            {
                return ShipComponentState.Worn
                        | (
                            currentState 
                            & (ShipComponentState.Active | ShipComponentState.Inactive | ShipComponentState.Disabled)
                        );
            }

            return currentState;
        }

        public void StartWear()
        {
            this.Start();
        }

        public void StopWear()
        {
            this.Stop();
        }

        public void OnComponentWearTimeout()
        {
            ApplyWear();
        }
    }
}