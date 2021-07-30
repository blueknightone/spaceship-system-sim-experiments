
using System.Collections.Generic;
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public class ShipComponent : Node, IShipComponent
    {
        #region Godot Signals

        [Signal] public delegate void ComponentStateChanged(ShipComponentState state);

        #endregion

        #region Public Properties

        /// <summary>
        ///     Component is active when the ShipComponentState.Active flag is on and Inoperable flags are off.
        /// </summary>
        public bool IsActive => (CurrentState & ShipComponentState.Active) != 0
                                && (CurrentState & ShipComponentState.Inoperable) == 0;

        public ShipComponentState CurrentState { get; private set; }

        #endregion

        #region Insepctor Variables

        [Export]
        private ShipComponentState startingState = ShipComponentState.Inactive;


        // TODO: Refactor incoming, outgoing, and internal buffers into ResourceStorageBuffer. Includ burn rate.

        /// <summary>
        ///     All the internal storage vessels of the component.
        /// </summary>
        [Export] private List<ShipConsumableStorage> internalStorages;

        [Export] private ComponentEfficiency componentEfficiency;

        [Export] private ComponentWear componentWear;

        #endregion

        #region Member Variables

        /// <summary>
        ///     References the <code>IShipSystem</code> that the component is attached to.
        /// </summary>
        private IShipSystem parentSystem;

        private Timer componentFunctionTimer;


        #endregion

        #region Godot Lifecycle Events

        public override void _Ready()
        {
            if (startingState == ShipComponentState.None)
            {
                GD.PushError($"{Name} starting state cannot be \"None\".");
            }
            else
            {
                CurrentState = startingState;
            }

            componentFunctionTimer = GetChild<Timer>(0);
            if (componentFunctionTimer is null)
            {
                GD.PushError($"{Name} must have a ComponentWearTimer.");
            }
        }

        public override void _Process(float delta)
        {
            ProcessState();
            if (!IsActive) return;

            ProcessWear();
            ProcessResources();
        }

        #endregion

        #region Public Methods

        public void SetComponentState(ShipComponentState nextState, bool overwriteState)
        {
            if (overwriteState)
            {
                CurrentState = nextState;
                return;
            }

            ShipComponentState newState = CurrentState | nextState;
        }

        /// <summary>
        ///     Checks if the component has enough of a given resource in its internal storage.
        /// </summary>
        /// <param name="resource">The resource to search for.</param>
        /// <param name="amountRequested">The amount of the resource desired.</param>
        /// <returns>Returns true if the amount of the requested resource is available.</returns>
        public bool CheckResourceAvailability(ShipConsumableResource resource, float amountRequested = 0f)
        {
            float totalAmountAvaialable = 0f;

            foreach (ShipConsumableStorage storage in internalStorages)
            {
                if (storage.AcceptedResource != resource) continue;

                totalAmountAvaialable += storage.CurrentAmountStored;

                // Stop iterating through storages if we found enough.
                if (totalAmountAvaialable > amountRequested) return true;
            }

            return false;
        }

        public float AddResource(ShipConsumableResource resource, float amountRequested)
        {
            float amountRemaining = amountRequested;

            foreach (ShipConsumableStorage buffer in internalStorages)
            {
                if (buffer.AcceptedResource != resource) continue;

                amountRemaining = buffer.AddToStorage(amountRemaining);

                if (amountRemaining.Approximately(0f)) return 0f;
            }

            return amountRemaining;
        }

        public float GetResource(ShipConsumableResource resource, float amountRequested)
        {
            float requestedAmountRemaining = amountRequested;
            float amountRetrieved = 0f;

            foreach (ShipConsumableStorage buffer in internalStorages)
            {
                if (buffer.AcceptedResource != resource) continue;

                amountRetrieved += buffer.RemoveFromStorage(requestedAmountRemaining);

                // If we're close enough to the requested amount, quit out early.
                if (amountRetrieved.Approximately(amountRequested)) return amountRetrieved;
            }

            return amountRetrieved;
        }

        public void Activate()
        {
            // If component is in any inoperable state, it cannot be activated.
            if ((CurrentState & ShipComponentState.Inoperable) != 0) return;

            // Unset the inactive bit
            CurrentState &= ~ShipComponentState.Inactive;

            //Set the active bit
            CurrentState |= ShipComponentState.Active;

            componentFunctionTimer.Start();
            EmitSignal(nameof(ComponentStateChanged), CurrentState);
        }

        public void Deactivate()
        {
            // Unset the Active state bit
            CurrentState &= ~ShipComponentState.Active;

            //Set the Inactive state bit.
            CurrentState |= ShipComponentState.Inactive;

            componentFunctionTimer.Stop();
            EmitSignal(nameof(ComponentStateChanged), CurrentState);
        }

        public void Toggle()
        {
            // If the current state is Inactive, activate the component.
            if (CurrentState.HasFlag(ShipComponentState.Inactive))
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }

        public void Damage(float wearAmount)
        {
            componentWear.AddWear(wearAmount);
            ProcessWear();
        }

        public void Destroy()
        {
            CurrentState |= ShipComponentState.Active;
            CurrentState &= ShipComponentState.Destroyed;
            EmitSignal(nameof(ComponentStateChanged), CurrentState);
        }

        public void Remove()
        {
            CurrentState = ShipComponentState.Inactive | ShipComponentState.Uninstalled;

            EmitSignal(nameof(ComponentStateChanged), CurrentState);
        }

        public void Install()
        {
            // If the copmonent was marked uninstalled (which it should be if it is not 
            //in a component socket), unset the Uninstalled state flag.
            if (CurrentState.HasFlag(ShipComponentState.Uninstalled))
            {
                CurrentState &= ~ShipComponentState.Uninstalled;
            }

            // Make sure that the component is not flagged as active (which it shouldn't be already).
            CurrentState &= ~ShipComponentState.Active;

            // Make sure the Inactive flag is set.
            CurrentState |= ShipComponentState.Inactive;

            EmitSignal(nameof(ComponentStateChanged), CurrentState);
        }

        public void ToggleOverclock()
        {
            if (CurrentState.HasFlag(ShipComponentState.Overclock))
            {
                CurrentState &= ~ShipComponentState.Overclock;
            }
            else
            {
                CurrentState |= ShipComponentState.Overclock;
            }
        }

        #endregion

        #region Private Methods

        private void ProcessState()
        {
            // If the current state is inoperbable, then we don't need to do anything.
            if (CurrentState.HasFlag(ShipComponentState.Inoperable)) return;

        }

        private void ProcessWear()
        {
            if (!IsActive) return;

            CurrentState = componentWear.GetWearState(CurrentState);
        }

        private void ProcessResources()
        {
            if (!IsActive) return;

            foreach (ShipConsumableStorage storage in internalStorages)
            {
                if (storage.FlowDirection == ComponentFlowDirection.In)
                {
                    storage.AddToStorage();
                }
                else
                {
                    storage.RemoveFromStorage();
                }
            }
        }

        #endregion
    }
}